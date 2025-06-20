using Engine.EventArgs;
using Engine.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {

        public event EventHandler<System.EventArgs> OnDeath;


        public void OnDeathEvent()
        {
            OnDeath?.Invoke(this, System.EventArgs.Empty);
        }
        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;

        public bool IsAlive => CurrentHitPoints > 0;
        public bool IsDead => !IsAlive;


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));

            }
        }

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }

        public List<GameItem> Weapons =>
                        Inventory.Where(i => i is Weapon)
                                 .GroupBy(i => i.Name)
                                 .Select(g => g.First())
                                 .ToList();

        protected LivingEntity()
        {
            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();


        }
        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
            if (item.IsUnique)
            {
                // if there are no grouped items with this id
                if (!GroupedInventory.Any(i => i.Item.ItemTypeID == item.ItemTypeID))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 1));
                }
            }
            else
            {
                if (GroupedInventory.Any(i => i.Item.ItemTypeID == item.ItemTypeID))
                {
                    GroupedInventory.First(i => i.Item.ItemTypeID == item.ItemTypeID)
                                    .Quantity += 1;
                }
                else
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 1));
                }
            }
            OnPropertyChanged(nameof(Weapons));
        }
        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
            GroupedInventoryItem groupedInventoryItem = GroupedInventory.First(i => i.Item.ItemTypeID == item.ItemTypeID);
            if (groupedInventoryItem != null)
            {
                if (groupedInventoryItem.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItem);
                }
                else
                {
                    groupedInventoryItem.Quantity -= 1;

                }
            }
            OnPropertyChanged(nameof(Weapons));
        }

        public void TakeDamage(int damage)
        {
            CurrentHitPoints -= damage;
            if (IsDead)
            {
                OnDeathEvent();
            }
        }
    }
}