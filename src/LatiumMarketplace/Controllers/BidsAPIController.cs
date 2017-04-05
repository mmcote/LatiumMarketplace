using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.BidViewModels;
using LatiumMarketplace.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LatiumMarketplace.Controllers
{
    /// <summary>
    /// Bid API Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/BidsAPIController")]
    public class BidsAPIController : Controller
    {
        private IBidRepository _BidRepository;
        private ApplicationDbContext _context;

        /// <summary>
        /// Bids API initial context and repo
        /// </summary>
        /// <param name="context">Sets the context</param>
        /// <param name="bidRepository">Set the bidRepo</param>
        public BidsAPIController(ApplicationDbContext context, IBidRepository bidRepository)
        {
            _context = context;
            _BidRepository = bidRepository; //new BidRepository(context);
        }

        /// <summary>
        /// GET: api/BidsAPI
        /// Get bids associated to an asset
        /// </summary>
        /// <param name="assetId">AssetID to GET</param>
        /// <returns>JSON object of Bid</returns>
        // GET: api/BidsAPI
        [HttpGet]
        public IActionResult GetBidsByAssetId([FromBody] int assetId)
        {
            IEnumerable<Bid> bids = null;
            try
            {
                bids = _BidRepository.GetBidsByAssetID(assetId);
            }
            catch (KeyNotFoundException)
            {
                bids = new List<Bid>();
            }
            catch
            {
                return new BadRequestObjectResult("Invalid Request");
            }
            return new OkObjectResult(bids.ToList());
        }
        
        /// <summary>
        /// GET all bids 
        /// </summary>
        /// <returns>JSON of bids</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Bid> bids = null;
            try
            {
                bids = _BidRepository.GetAll();
            }
            catch (KeyNotFoundException)
            {
                bids = new List<Bid>();
            }
            catch
            {
                return new BadRequestObjectResult("Invalid Result");
            }
            return new OkObjectResult(bids.ToList());
        }

        /// <summary>
        /// Get bid by ID
        /// </summary>
        /// <param name="id">ID of specific bid</param>
        /// <returns>JSON of bid</returns>
        // GET api/BidsAPI/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var bidRepo = _BidRepository.GetBidByID(id);
            return "value";
        }


        /// <summary>
        /// Get id of user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>JSON of bids of a user</returns>
        // GETMINE api/BidsAPI/5
        [HttpGet("{id}")]
        public IActionResult GetMine( string id)
        {
            IEnumerable<Bid> bids = null;
            try
            {
                bids = _BidRepository.GetMyBids(id);
            }
            catch (KeyNotFoundException)
            {
                bids = new List<Bid>();
            }
            catch
            {
                return new BadRequestObjectResult("Invalid Request");
            }
            return new OkObjectResult(bids.ToList());
        }

        /// <summary>
        /// Get bids of other user
        /// </summary>
        /// <param name="id">ID of specific user</param>
        /// <returns>HTTP Response, JSON of bids of other user</returns>
        [HttpGet("{id}")]
        public IActionResult GetOthers(string id)
        {
            IEnumerable<Bid> bids = null;
            try
            {
                bids = _BidRepository.GetOthersBids(id);
            }
            catch (KeyNotFoundException)
            {
                bids = new List<Bid>();
            }
            catch
            {
                return new BadRequestObjectResult("Invalid Request");
            }
            return new OkObjectResult(bids.ToList());
        }

        [HttpPost("email")]
        [Route("api/BidsAPIController/GetAssetOwnerNotificationCount")]
        public IActionResult GetAssetOwnerNotificationCount([FromBody]string email)
        {
            int count = _context.Bid.Include(b => b.asset).Where(b => b.asset.ownerName == email && b.assetOwnerNotificationPending == true).Count();
            return new OkObjectResult(count);
        }

        [HttpPost("email")]
        [Route("api/BidsAPIController/GetBidderNotificationCount")]
        public IActionResult GetBidderNotificationCount([FromBody]string email)
        {
            int count = _context.Bid.Where(b => b.bidder == email && b.bidderNotificationPending == true).Count();
            return new OkObjectResult(count);
        }

        // POST api/BidsAPI
        /// <summary>
        /// POST method for bids
        /// </summary>
        /// <param name="bid">BID</param>
        /// <returns>HTTP Response</returns>
        [HttpPost]
        public IActionResult Post([FromBody]Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _BidRepository.AddBid(bid);
            try
            {
                 _BidRepository.Save();
            }
            catch
            {
                throw;
            }
            return NoContent();
        }

        /// <summary>
        /// PUT bid value
        /// </summary>
        /// <param name="id">Bid ID</param>
        /// <param name="value">Price of bid</param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete a specific bid
        /// </summary>
        /// <param name="id">Bid ID</param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        { 
            _BidRepository.DeleteBid(id);
        }
    }
}
