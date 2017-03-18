using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.BidViewModels
{
    public interface IBidRepository
    {
        void AddBid(Bid bid);
        void DeleteBid(Guid guid);
        IEnumerable<Bid> GetBidsByAssetID(int id);
        void Save();
    }
}
