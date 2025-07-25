﻿using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest : BaseNotificationClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemQuantity> ItemToComplete { get; set; }
        public string AllItemsToComplete { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardItems { get; set; }
        public string AllRewardItems { get; set; }


        private bool _isCompleted;
        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
        public Quest(int id, string name, string description, List<ItemQuantity> itemsToComplete,
                      int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems, bool isCompleted = false)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemToComplete = itemsToComplete;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
            IsCompleted = isCompleted;
        }

    }
}
