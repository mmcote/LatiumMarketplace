using LatiumMarketplace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.BidViewModels
{
    /// <summary>
    /// Repository for the Bids
    /// Contains basic add, delete functions, as well as other query functions
    /// </summary>
    public class BidRepository : IBidRepository
    {
        private ApplicationDbContext _context;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a bid 
        /// </summary>
        /// <param name="bid"></param>
        public void AddBid(Bid bid)
        {
            _context.Add(bid);
            return;
        }
        /// <summary>
        /// Deletes a bid
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBid(int id)
        {

            if (id == 0)
            {
                throw new ArgumentNullException("The Id given.");
            }

            var bids = _context.Bid.Single(m => m.bidId == id);
            if (bids == null)
            {
                throw new Exception("No bid found by the id");
            };
            _context.Bid.Remove(bids);
            return;
        }

        /// <summary>
        /// GetAll - returns all the bids
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bid> GetAll()
        {
            return _context.Bid.ToList();
        }
        /// <summary>
        /// GetBidsByAssetID returns all the bids associate with a specific asset
        /// </summary>
        /// <param name="asset_id"></param>
        /// <returns></returns>
        public IEnumerable<Bid> GetBidsByAssetID(int asset_id)
        {
            if (asset_id == 0)
            {
                throw new ArgumentNullException("The asset id is null");
            }

            var bids = _context.Bid.Where(m => m.asset.assetID == asset_id);
            if (bids == null)
            {
                throw new KeyNotFoundException("No matching bids foudn by the given assetid");
            }
            return bids;
        }

        /// <summary>
        /// GetBidByID - returns a bid that matches the id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bid GetBidByID(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("This bid id is null");
            }

            var bid = _context.Bid.Single(m => m.bidId == id);
            if (bid == null)
            {
                throw new KeyNotFoundException("No matching bid found with given bidId");
            }
            return bid;
        }
        /// <summary>
        /// GetMyBids - returns a list of bids that the user made
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Bid> GetMyBids(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("The user name is null");
            }
            var bids = _context.Bid.Where(m => m.bidder == name);
            if (bids == null)
            {
                throw new KeyNotFoundException("No matching name found by username ");
            }

            return bids;
        }

        /// <summary>
        /// GetOthersBids - returns a list of bids that others have placed on your assets
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Bid> GetOthersBids(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("The id is null");
            }
            var bids = _context.Bid.Where(m => m.asset.ownerID == id);
            if (bids == null)
            {
                throw new KeyNotFoundException("No matching id found by id");
            }
            return bids;

        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
