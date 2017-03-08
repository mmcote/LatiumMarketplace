using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class AssetLocation
    {
        public List<Asset> assets;
        public SelectList locations;
        public string assetLocation { get; set; }
    }
}
