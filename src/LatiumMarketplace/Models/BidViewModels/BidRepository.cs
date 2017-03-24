using LatiumMarketplace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.BidViewModels
{
    public class BidRepository : IBidRepository
    {
        private ApplicationDbContext _context;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBid(Bid bid)
        {
            _context.Add(bid);
            return;
        }

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

        public IEnumerable<Bid> GetAll()
        {
            return _context.Bid.ToList();
        }

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
