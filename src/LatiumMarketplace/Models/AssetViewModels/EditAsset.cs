using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class EditAsset
    {
        public EditAsset(Asset asset, IEnumerable<Make> makes)
        {
            editableAsset = asset;
            Makes = makes;
        }
        public Asset editableAsset { get; set; }
        public IEnumerable<Make> Makes { get; set; }
    }
}
