using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.BidViewModels
{
    public interface IBidRepository
    {
        void AddBid(Bid bid);
        void DeleteBid(int id);
        Bid GetBidByID(int id);
        IEnumerable<Bid> GetBidsByAssetID(int asset_id);
        IEnumerable<Bid> GetMyBids( string name);
        IEnumerable<Bid> GetOthersBids(string id);
        IEnumerable<Bid> GetAll();
        void Save();
    }
}
