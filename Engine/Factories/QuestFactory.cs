using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 2));
            itemsToComplete.Add(new ItemQuantity(9002, 2));
            rewardItems.Add(new ItemQuantity(1002, 1));
            _quests.Add(new Quest(1,
                                  "Fight in the Botanic Garden",
                                  "Find the food that sneaky Trash Panda stole earlier.",
                                  itemsToComplete,
                                  25, 10,
                                  rewardItems));

            itemsToComplete = new List<ItemQuantity>();
            rewardItems = new List<ItemQuantity>();
            itemsToComplete.Add(new ItemQuantity(9003, 2));
            itemsToComplete.Add(new ItemQuantity(9004, 2));
            rewardItems.Add(new ItemQuantity(1003, 1));

            _quests.Add(new Quest(2,
                      "Hunt in the High Park",
                      "Defeat Coyotes in the park to protect the community!",
                      itemsToComplete,
                      50, 20,
                      rewardItems));

            itemsToComplete = new List<ItemQuantity>();
            rewardItems = new List<ItemQuantity>();
            itemsToComplete.Add(new ItemQuantity(9005, 2));
            itemsToComplete.Add(new ItemQuantity(9006, 2));
            rewardItems.Add(new ItemQuantity(1004, 1));

            _quests.Add(new Quest(3,
                      "Rescue in the Tommy Thompson Park",
                      "Hikers in Tommy Thompson Park are surrounded by Rabid Geese. \n" +
                      "Eliminate Rabid Geese to protect the stranded hikers!",
                      itemsToComplete,
                      50, 20,
                      rewardItems));
        }
        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
