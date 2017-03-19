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

        public BidsAPIController(ApplicationDbContext context)
        {
            _context = context;
            _BidRepository = new BidRepository(context);
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
            //string myString = id.ToString();
            //Guid guid = Guid.Parse(myString); 
            //_BidRepository.DeleteBid(guid);
        }
    }
}
