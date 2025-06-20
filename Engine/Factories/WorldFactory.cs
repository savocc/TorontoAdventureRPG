using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            newWorld.AddLocation(-2, 0, "High Park",
                "There are Cherry Blosson trees growing in here, with coyotes hiding among them.",
                "HighPark.png");

            // Adding a coyote
            newWorld.LocationAt(-2, 0).AddMonster(2, 100);

            newWorld.AddLocation(-1, 0, "Hunter's House",
                "This is the house of your neighbor, Farmer Ted.",
                "Hunterhouse.png");

            newWorld.LocationAt(-1, 0).TraderHere = TraderFactory.GetTraderByName("Farmer Greg");

            newWorld.LocationAt(-1, 0).QuestsAvailibleHere
                                      .Add(QuestFactory.GetQuestByID(2));

            newWorld.AddLocation(0, 0, "House",
                "This is your home",
                "House.png");
            newWorld.AddLocation(-1, 1, "Trading Shop",
                "The shop of Susan, the trader.",
                "TradingShop.png");

            newWorld.LocationAt(-1, 1).TraderHere = TraderFactory.GetTraderByName("Susan");

            newWorld.AddLocation(0, 1, "Brick Works",
                "You see an ancient factory here. Who knows what's going on inside these walls?",
                "BrickFactory.png");
            newWorld.AddLocation(1, 0, "Harbour Lookout",
                "You notice hikers on the opposite shore. They look frightened!",
                "HarbourLookout.png");

            newWorld.LocationAt(1, 0).QuestsAvailibleHere
                                      .Add(QuestFactory.GetQuestByID(3));

            newWorld.AddLocation(2, 0, "Tommy Thompson Park",
                "Don't go far into the forest, you may not come back.",
                "TommyThompson.png");

            newWorld.LocationAt(2, 0).AddMonster(3, 100);


            newWorld.LocationAt(0, 1) // returns location object
                    .QuestsAvailibleHere // accesses quest list for this location
                    .Add(QuestFactory.GetQuestByID(1)); // adds an item to list. ( finds by id)

            newWorld.AddLocation(0, 2, "Botanic Garden",
                "There are many plants here, with trash pandas hiding behind them.",
                "BotanicGarden.png");
            newWorld.LocationAt(0, 2).AddMonster(1, 100);
            return newWorld;
        }
    }
}
