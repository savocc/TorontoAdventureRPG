using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Models;
namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();
        static TraderFactory()
        {
            Trader susan = new Trader("Susan");
            susan.AddItemToInventory(ItemFactory.GetRandomItem());
            susan.AddItemToInventory(ItemFactory.GetRandomItem());

            Trader farmerTed = new Trader("Farmer Greg");
            farmerTed.AddItemToInventory(ItemFactory.GetRandomItem());
            farmerTed.AddItemToInventory(ItemFactory.GetRandomItem());

            AddTraderToList(susan);
            AddTraderToList(farmerTed);

        }
        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }
        private static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named '{trader.Name}'");
            }
            _traders.Add(trader);
        }
    }
}