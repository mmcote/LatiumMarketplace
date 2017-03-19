using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models.BidViewModels;
using Microsoft.AspNetCore.Identity;
using LatiumMarketplace.Models;

namespace LatiumMarketplace.Controllers
{
    public class BidsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BidsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;    
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bid.ToListAsync());
        }

        // GET: Bids/Details/5
       /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid.SingleOrDefaultAsync(m => m.bidId == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }*/

        // GET: Bids/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bidId,bidPrice,description,endDate,startDate,bidder")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userId = user?.Id;
                var userName = user?.UserName;
                bid.bidder = userName;
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bid);
        }

        

                // GET: Bids/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var bid = await _context.Bid.SingleOrDefaultAsync(m => m.bidId == id);
                    if (bid == null)
                    {
                        return NotFound();
                    }

                    return View(bid);
                }

                // POST: Bids/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var bid = await _context.Bid.SingleOrDefaultAsync(m => m.bidId == id);
                    _context.Bid.Remove(bid);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                } 

        private bool BidExists(int id)
        {
            return _context.Bid.Any(e => e.bidId == id);
        }
    }
}
