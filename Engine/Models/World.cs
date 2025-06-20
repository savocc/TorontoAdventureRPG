using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>();

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            Location loc = new Location();
            loc.XCoordinate = xCoordinate;
            loc.YCoordinate = yCoordinate;
            loc.Name = name;
            loc.Description = description;
            loc.ImageName = $"/Images/Locations/{imageName}";
            _locations.Add(loc);

        }

        internal void AddMonsterToLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            Location loc = LocationAt(xCoordinate, yCoordinate);
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location location in _locations)
            {
                if (location.XCoordinate == xCoordinate && location.YCoordinate == yCoordinate)
                    return location;
            }
            return null;

        }
    }
}
