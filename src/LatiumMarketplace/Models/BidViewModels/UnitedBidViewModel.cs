using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Models.BidViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models
{
    public class UnitedBidViewModel
    {
        public IEnumerable<Bid> bidModel { get; set; }
        public  List<Asset> assetModel { get; set; }
    }
}
