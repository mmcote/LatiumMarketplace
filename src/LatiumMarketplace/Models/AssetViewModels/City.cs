using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    /// <summary>
    /// This reresents the city where an asset is located.
    /// </summary>
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }

        // Navigation properties for one-to-many relationship between Asset and City.
        // One city can have many assets
        public ICollection<Asset> Assets { get; set; }
    }
}
