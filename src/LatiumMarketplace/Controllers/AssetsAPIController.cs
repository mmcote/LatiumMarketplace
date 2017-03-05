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
    [Produces("application/json")]
    [Route("api/AssetsAPI")]
    public class AssetsAPIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AssetsAPI
        [HttpGet]
        public IEnumerable<Asset> GetAsset()
        {
            return _context.Asset;
        }

        // GET: api/AssetsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsset([FromRoute] int id)
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

            return Ok(asset);
        }

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

        // POST: api/AssetsAPI
        [HttpPost]
        public async Task<IActionResult> PostAsset([FromBody] Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Asset.Add(asset);
            try
            {
                await _context.SaveChangesAsync();
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

            return CreatedAtAction("GetAsset", new { id = asset.assetID }, asset);
        }

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

            return Ok(asset);
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.assetID == id);
        }
    }
}