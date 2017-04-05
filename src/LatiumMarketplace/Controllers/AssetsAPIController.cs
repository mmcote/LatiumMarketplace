using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Controllers
{
    /// <summary>
    /// Asset API
    /// </summary>
    [Produces("application/json")]
    [Route("api/AssetsAPI")]
    public class AssetsAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// Assets API initialize context
        /// </summary>
        /// <param name="context">set context of ApplicationDbContext</param>
        public AssetsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static Random rng = new Random();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// POST method for asset category
        /// </summary>
        /// <param name="category">Category for POST request</param>
        /// <returns>HTTP response for POST category</returns>
        // POST: api/PostCategory
        [HttpPost("GetRandomCategories")]
        [Route("api/AssetsAPI/GetRandomCategories")]

        public IActionResult GetRandomCategories()
        {
            var allCategories = _context.Category.ToList();
            Shuffle(allCategories);
            return new OkObjectResult(allCategories);
        }

        /// <summary>
        /// GET method for asset
        /// </summary>
        /// <returns>HTTP Response of GET request</returns>
        // GET: api/AssetsAPI
        [HttpGet]
        public IActionResult GetAsset()
        {
            var list = _context.Asset.ToList();
            return new OkObjectResult(list);
        }

        /// <summary>
        /// GET method for asset
        /// </summary>
        /// <param name="id">ID of asset</param>
        /// <returns>JSON of asset</returns>
        // GET: api/AssetsAPI/5
        [HttpGet("{id}")]
        public IActionResult GetAsset([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Asset asset = _context.Asset.Single(m => m.assetID == id);

            if (asset == null)
            {
                return NotFound();
            }

            return new OkObjectResult(asset);
        }

        /// <summary>
        /// PUT method for asset
        /// </summary>
        /// <param name="id">ID of asset to put</param>
        /// <param name="asset">Asset input for put request</param>
        /// <returns>HTTP Response</returns>
        // PUT: api/AssetsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset([FromRoute] int id, [FromBody] Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset.assetID)
            {
                return BadRequest();
            }

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// POST method for asset
        /// </summary>
        /// <param name="asset">Asset for post request</param>
        /// <returns>HTTP Response for post</returns>
        // POST: api/AssetsAPI
        [HttpPost("PostAsset")]
        public IActionResult PostAsset([FromBody] Asset asset)
        {
            _context.Asset.Add(asset);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AssetExists(asset.assetID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(asset);
        }

        /// <summary>
        /// Delete method for asset
        /// </summary>
        /// <param name="id">ID for Asset</param>
        /// <returns>HTTP Response for DELETE request</returns>
        // DELETE: api/AssetsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Asset asset = await _context.Asset.SingleOrDefaultAsync(m => m.assetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            _context.Asset.Remove(asset);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.assetID == id);
        }

        /// <summary>
        /// POST method for asset category
        /// </summary>
        /// <param name="category">Category for POST request</param>
        /// <returns>HTTP response for POST category</returns>
        // POST: api/PostCategory
        [HttpPost("PostCategory")]
        public IActionResult PostCategory([FromBody]Category category)
        {
            _context.Category.Add(category);
            var success = _context.SaveChanges();
            if (success == 0)
            {
                return new BadRequestResult();
            }
            return new OkResult();
        }

        /// <summary>
        /// POST method for asset make
        /// </summary>
        /// <param name="make">asset make for post method</param>
        /// <returns>HTTP Response for post method</returns>
        // POST: api/PostMake
        [HttpPost("PostMake")]
        public IActionResult PostMake([FromBody] string make)
        {
            Make makename = new Make();
            makename.Name = make;
            _context.Make.Add(makename);
            var success = _context.SaveChanges();
            if (success == 0)
            {
                return new BadRequestResult();
            }
            return new OkResult();
        }

        /// <summary>
        /// Post method for asset city
        /// </summary>
        /// <param name="city">City for asset post request</param>
        /// <returns>HTTP Response City Post</returns>
        // POST: api/PostCity
        [HttpPost("PostCity")]
        public IActionResult PostCity([FromBody] string city)
        {
            City cityname = new City();
            cityname.Name = city;
            _context.City.Add(cityname);
            var success = _context.SaveChanges();
            if (success == 0)
            {
                return new BadRequestResult();
            }
            return new OkResult();
        }
    }
}