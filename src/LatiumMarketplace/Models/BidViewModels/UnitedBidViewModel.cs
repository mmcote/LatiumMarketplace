using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Models.BidViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models
{
    /// <summary>
    /// UnifiedBidViewModel is a "Super Model" that contains 2 submodels
    /// Asset and Bid. The sub model contains a list of asset, and 2 lists
    /// of bids that are filtered.
    /// </summary>
    public class UnitedBidViewModel
    {
        public List<Asset> assetModel { get; set; }
        public List<Bid> inbox { get; set; }
        public List<Bid> outbox { get; set; }
    }
}
