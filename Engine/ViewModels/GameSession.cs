using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using System.ComponentModel;
using Engine.EventArgs;
using System.Runtime.CompilerServices;
namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;


        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnDeath += OnPlayerDeath;
                }
            }
        }


        private Location _currentLocation;
        private Trader _currentTrader;
        public Location CurrentLocation
        {
            get
            {
                return _currentLocation;
            }
            set
            {
                _currentLocation = value;

                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToTheNorth));
                OnPropertyChanged(nameof(HasLocationToTheEast));
                OnPropertyChanged(nameof(HasLocationToTheSouth));
                OnPropertyChanged(nameof(HasLocationToTheWest));
                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
                CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        public List<Quest> AllAvailibleQuests = new List<Quest>();

        public Trader CurrentTrader
        {
            get
            {
                return _currentTrader;
            }
            set
            {
                _currentTrader = value;
                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }
        // Expression body
        public bool HasMonster => CurrentMonster != null;
        private Monster _monster;
        public Monster CurrentMonster
        {
            get
            {
                return _monster;
            }
            set
            {
                _monster = value;
                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
                if (CurrentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here");
                }
            }
        }
        public Weapon CurrentWeapon { get; set; }
        public World CurrentWorld { get; set; }

        public bool HasLocationToTheNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        public bool HasLocationToTheEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        public bool HasLocationToTheSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;

        public bool HasLocationToTheWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        public bool HasTrader => CurrentTrader != null;

        public GameSession()
        {
            // named parameters
            CurrentPlayer = new Player
            {
                Name = "Maple",
                CharacterClass = "Fighter",
                CurrentHitPoints = 10,
                MaximumHitPoints = 10,
                Gold = 10,
                ExperiencePoints = 0,
                Level = 1
            };
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);

            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

        }


        public void MoveNorth()
        {
            if (HasLocationToTheNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        public void MoveEast()
        {
            if (HasLocationToTheEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);

            }

        }
        public void MoveWest()
        {
            if (HasLocationToTheWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);

            }

        }
        public void MoveSouth()
        {
            if (HasLocationToTheSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);

            }

        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailibleHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(quest);
                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest");
                    RaiseMessage(quest.Description);
                    RaiseMessage("Return with:");
                    string allItemsToComplete = "";
                    foreach (ItemQuantity itemQuantity in quest.ItemToComplete)
                    {
                        allItemsToComplete += $"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}\n";
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                    quest.AllItemsToComplete = allItemsToComplete;
                    RaiseMessage("And you will receive:");
                    RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"   {quest.RewardGold} gold");
                    string allRewardItems = "";
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        allRewardItems += $"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}\n";
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                    quest.AllRewardItems = allRewardItems;

                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void AttackCurrentMonster()
        {
            if (CurrentWeapon == null)
            {
                RaiseMessage("You must have a weapon to attack");
                return;
            }
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);
            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}");
            }
            else
            {
                CurrentMonster.TakeDamage(damageToMonster); ;
                RaiseMessage($"You hit the {CurrentMonster.Name}  for {damageToMonster} points.");
            }
            if (CurrentMonster.CurrentHitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");
                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
                CurrentPlayer.Gold += CurrentMonster.Gold;
                RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
                foreach (GameItem gameItem in CurrentMonster.Inventory)
                {
                    CurrentPlayer.AddItemToInventory(gameItem);
                    RaiseMessage($"You receive one {gameItem.Name}.");
                }
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // If monster is still alive, let the monster attack
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);
                if (damageToPlayer == 0)
                {
                    RaiseMessage("The monster attacks, but misses you.");
                }
                else
                {
                    CurrentPlayer.TakeDamage(damageToPlayer);
                    RaiseMessage($"The Monster hit you for {damageToPlayer} points.");
                }
            }
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));

        }

        public void OnPlayerDeath(object sender, System.EventArgs e)
        {
            RaiseMessage("");
            RaiseMessage($"The Monster killed you.");
            CurrentLocation = CurrentWorld.LocationAt(0, 0); // Player's home
            CurrentPlayer.CurrentHitPoints = CurrentPlayer.Level * 10; // Completely heal the player
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailibleHere)
            {
                Quest questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.ID == quest.ID && !q.IsCompleted);
                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }
                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");
                        // Give the player the quest rewards
                        CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold");
                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"You receive a {rewardItem.Name}");
                        }
                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }


        }


    }
}