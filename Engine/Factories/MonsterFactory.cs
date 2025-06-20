using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch (monsterID)
            {
                case 1:
                    Monster trashPanda =
                        new Monster("Trash Panda", "TrashPanda.png", 4, 4, 1, 2, 5, 1);
                    AddLootItem(trashPanda, 9001, 25);
                    AddLootItem(trashPanda, 9002, 75);
                    return trashPanda;
                case 2:
                    Monster coyote =
                        new Monster("Combative Coyote", "Coyote.png", 20, 20, 1, 10, 10, 5);
                    AddLootItem(coyote, 9003, 25);
                    AddLootItem(coyote, 9004, 75);
                    return coyote;
                case 3:
                    Monster canadaGoose =
                        new Monster("Rabid Goose", "CanadaGoose.png", 30, 30, 1, 30, 20, 10);
                    AddLootItem(canadaGoose, 9005, 25);
                    AddLootItem(canadaGoose, 9006, 75);
                    return canadaGoose;
                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }
        }
        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }
    }
}
