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


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
