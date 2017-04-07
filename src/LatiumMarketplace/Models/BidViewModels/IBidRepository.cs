using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.BidViewModels
{
    /// <summary>
    /// Interface for Bids Repository
    /// Contains basic add/delete/get bid functions
    /// list of bids by asset
    /// list of the bids the user made
    /// list of the bids other have made on the users assets
    /// </summary>
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
