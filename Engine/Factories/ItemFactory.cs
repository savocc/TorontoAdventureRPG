using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        // Not updatatable, only in constructor
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();

        // firt function that will run as soon as anyone uses any of class's items
        static ItemFactory()
        {

            _standardGameItems.Add(new Weapon(1001, "Pointy Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Eerie Poison", 15, 5, 10));
            _standardGameItems.Add(new Weapon(1003, "Spiked Mace", 50, 10, 20));
            _standardGameItems.Add(new Weapon(1004, "Wicked Potion", 100, 20, 50));

            _standardGameItems.Add(new GameItem(9001, "Rotten Banana", 1));
            _standardGameItems.Add(new GameItem(9002, "Smelly Sausage", 2));
            _standardGameItems.Add(new GameItem(9003, "Dog's Collar", 1));
            _standardGameItems.Add(new GameItem(9004, "Infectious Rat", 2));
            _standardGameItems.Add(new GameItem(9005, "Contaminated Earthworms", 1));
            _standardGameItems.Add(new GameItem(9006, "Freshwater snails", 2));
        }
        public static GameItem GetRandomItem()
        {
            if (_standardGameItems.Count != 0)
            {
                Random rnd = new Random();
                return _standardGameItems.ElementAt(rnd.Next(0, _standardGameItems.Count));
            }

            return null;
        }

        public static GameItem CreateGameItem(int itemTypeId)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeId);
            if (standardItem != null)
            {
                if (standardItem is Weapon)
                {
                    return (standardItem as Weapon).Clone();

                }
                return standardItem.Clone();
            }
            return null;
        }
    }
}
