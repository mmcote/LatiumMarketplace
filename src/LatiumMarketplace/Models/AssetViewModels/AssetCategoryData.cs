using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class AssetCategoryData
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public SelectList CategoryList { get; set; }
    }
}
