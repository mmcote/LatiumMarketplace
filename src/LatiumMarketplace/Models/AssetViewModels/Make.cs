using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Make
    {
        public int MakeId { get; set; }
        public string Name { get; set; }

        // Navigation properties for one-to-many relationship between
        // Asset and Make.
        // One asset can only have one make, but many makes can be shared by
        // multiple assets.
        public ICollection<Asset> Assets { get; set; }
    }
}
