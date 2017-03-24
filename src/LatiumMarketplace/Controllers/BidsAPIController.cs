using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.BidViewModels;
using LatiumMarketplace.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LatiumMarketplace.Controllers
{

    [Produces("application/json")]
    [Route("api/BidsAPIController")]
    public class BidsAPIController : Controller
    {
        private IBidRepository _BidRepository;
        private ApplicationDbContext _context;

        public BidsAPIController(ApplicationDbContext context, IBidRepository bidRepository)
        {
            _context = context;
            _BidRepository = bidRepository; //new BidRepository(context);
        }


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


        // GET api/BidsAPI/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var bidRepo = _BidRepository.GetBidByID(id);
            return "value";
        }


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

        // POST api/BidsAPI
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


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        { 
            _BidRepository.DeleteBid(id);
        }
    }
}
