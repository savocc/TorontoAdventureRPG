using Engine.EventArgs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties
        private string _characterClass;
        private int _experiencePoints;
        public event EventHandler<GameMessageEventArgs> OnLevelUpRaised;


        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
                LevelUp();

            }
        }



        private void LevelUp()
        {
            int originalLevel = Level;
            if (ExperiencePoints / 100 + 1 != Level)
            {
                Level++;

                RaiseMessageOnLevelUp("");
                RaiseMessageOnLevelUp($"You reached Level {Level}!");
                RaiseMessageOnLevelUp("");
            }
        }


        public ObservableCollection<Quest> Quests { get; set; }
        #endregion
        public Player()
        {
            Quests = new ObservableCollection<Quest>();
        }
        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        private void RaiseMessageOnLevelUp(string message)
        {
            OnLevelUpRaised?.Invoke(this, new GameMessageEventArgs(message));
        }


    }
}