using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class AssetIndexData
    {
        public IEnumerable<Asset> Assets { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Image> Images { get; set; }
        
    }
}
