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
using LatiumMarketplace.Models.AssetViewModels;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Create([Bind("bidId,bidPrice,description,endDate,startDate,bidder")] Bid bid, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string id = HttpContext.Request.Cookies["assetId"];
                int asset_id = Int32.Parse(id);
                Asset asset = _context.Asset.Single(a => a.assetID == asset_id);
                bid.asset = asset;
                bid.asset_id_model = asset_id;
                bid.asset_name = asset.name;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userId = user?.Id;
                var userName = user?.UserName;
                bid.bidder = userName;
                _context.Add(bid);
                await _context.SaveChangesAsync();
                
                RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Assets", new { @Id = asset_id });
                return redirectResult;
            }
            return View(bid);
        }

        //Listing of assets/requests belonging to a specific user
        [AllowAnonymous]
        public async Task<IActionResult> MyBids()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user?.Id;
            var MyAssets = _context.Bid.Where(s => s.asset.ownerID == userId); //shows only his assets that have bids on them
            var MyBids = _context.Bid.Where(s => s.bidder == user.UserName); // everything you bid on

            // gets all his assets 
            List<Asset> asset_list = new List<Asset>();
            var myAset = _context.Asset.Where(s => s.assetID != 0); // get all assets
            foreach( var a in myAset)
            {
                if (a.ownerID == userId)
                {
                    asset_list.Add(a);
                }
            }
            // All posts that you made
            List<Bid> inbox_list = new List<Bid>();
            foreach (var item in MyAssets)
            {
                if (item.asset.ownerID == userId)
                {
                    inbox_list.Add(item);
                }
            }            

            // All post that you bid on
            List<Bid> outbox_list = new List<Bid>();
            foreach (var item in MyBids)
            {
                if (item.bidder == user.UserName)
                {
                    outbox_list.Add(item);
                }
            }

            UnitedBidViewModel completeBidModel = new UnitedBidViewModel();
            completeBidModel.assetModel = asset_list;
            completeBidModel.inbox = inbox_list;
            completeBidModel.outbox = outbox_list;
            await _context.SaveChangesAsync();
            return View(completeBidModel); 
            //return View(await _context.Bid.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> MyBid()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user?.Id;

            var MyAssets = _context.Bid.Where(s => s.asset.ownerID == userId);
            ViewBag.Asset = MyAssets;
            var MyBid = _context.Bid.Where(s => s.bidder == user.UserName);
            ViewBag.Bid = MyBid;

            return View();
        }




        // GET: Bids/Delete/5
        public async Task<IActionResult> Delete(int id)
                {
                    if (id == 0)
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
