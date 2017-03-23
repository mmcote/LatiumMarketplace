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
using Microsoft.AspNetCore.SignalR.Infrastructure;
using LatiumMarketplace.Hubs;

namespace LatiumMarketplace.Controllers
{
    public class BidsController : ApiHubController<Broadcaster>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BidsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConnectionManager connectionManager)
            : base(connectionManager)
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
                string id = HttpContext.Request.Cookies["assetId"];
                int asset_id = Int32.Parse(id);
                Asset asset = _context.Asset.Single(a => a.assetID == asset_id);
                bid.asset = asset;
                bid.asset_id_model = asset_id;
                bid.asset_name = asset.name;
                bid.status = asset.request;
                bid.chosen = false;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userId = user?.Id;
                var userName = user?.UserName;
                bid.bidder = userName;
                _context.Add(bid);
                await _context.SaveChangesAsync();

                // This notification redirect URL should put the user to the discussion
                string redirectURL = "/Bids/MyBids/";
                Notification notification = new Notification(bid.asset_name, 
                    "There has been a new bid placed on your asset, "+bid.asset_name+".", redirectURL);
                notification.type = 1;
                string notificationEmail = _context.User.Single(u => u.Id == bid.asset.ownerID).Email;
                Clients.Group(notificationEmail).AddNotificationToQueue(notification);

                RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Assets", new { @Id = asset_id });
                return redirectResult;
            }
            return View(bid);
        }

        //Listing of assets/requests belonging to a specific user
        [AllowAnonymous]
        public async Task<IActionResult> MyBids(string sortby)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user?.Id;
            var MyBids = _context.Bid.Where(s => s.bidder == user.UserName); // everything you bid on
            var OtherBids = _context.Bid.Where(s => s.asset.ownerID == userId); //shows only his assets that have bids on them
            var my_Assets = _context.Asset.Where(s => s.assetID != 0); // get all assets

            var assets = from m in my_Assets
                         select m;
            switch (sortby)
            { 
                case "request":
                    assets = assets.Where(s => s.request.Equals(true));
                    break;
                case "asset":
                    assets = assets.Where(s => s.request.Equals(false));
                    break;
                case "all":
                    assets = from m in my_Assets
                             select m;
                    break;
            }

            var otherBids = from m in OtherBids
                         select m;
            switch (sortby)
            {
                case "request":
                    otherBids = otherBids.Where(s => s.status.Equals(true));
                    break;
                case "asset":
                    otherBids = otherBids.Where(s => s.status.Equals(false));
                    break;
                case "all":
                    otherBids = from m in OtherBids
                             select m;
                    break;
            }

            var myBids = from m in MyBids
                         select m;
            switch (sortby)
            {

                case "request":

                    myBids = myBids.Where(s => s.status.Equals(true));
                    break;
                case "asset":
                    myBids = myBids.Where(s => s.status.Equals(false));
                    break;
                case "all":
                    myBids = from m in MyBids
                             select m;
                    break;
            }

            List <Asset> list_asset = new List<Asset>();
            foreach (var a in assets)
            {
                list_asset.Add(a);
            }

            // gets all his assets 
            List<Asset> asset_list = new List<Asset>();
            foreach (var a in list_asset)
            {
                if (a.ownerID == userId)
                {
                    asset_list.Add(a);
                }
            }
            // All posts that you made INBOX
            List<Bid> inbox_list = new List<Bid>();
            foreach (var b in otherBids)
            {
                inbox_list.Add(b);
               
            }
            // All post that you bid on OUTBOX
            List<Bid> outbox_list = new List<Bid>();
            foreach (var item in myBids)
            {
                outbox_list.Add(item);
            }
            UnitedBidViewModel completeBidModel = new UnitedBidViewModel();
            completeBidModel.assetModel = asset_list;
            completeBidModel.outbox = outbox_list;
            completeBidModel.inbox = inbox_list;
            await _context.SaveChangesAsync();
            return View(completeBidModel); 
        }


        //Listing of assets/requests belonging to a specific user
        [AllowAnonymous]
        public async Task<IActionResult> Inbox(string sortby)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user?.Id;
            var MyBids = _context.Bid.Where(s => s.bidder == user.UserName); // everything you bid on
            var OtherBids = _context.Bid.Where(s => s.asset.ownerID == userId); //shows only his assets that have bids on them
            var my_Assets = _context.Asset.Where(s => s.assetID != 0); // get all assets

            var assets = from m in my_Assets
                         select m;
            switch (sortby)
            {
                case "request":
                    assets = assets.Where(s => s.request.Equals(true));
                    break;
                case "asset":
                    assets = assets.Where(s => s.request.Equals(false));
                    break;
                case "all":
                    assets = from m in my_Assets
                             select m;
                    break;
            }

            var otherBids = from m in OtherBids
                            select m;
            switch (sortby)
            {
                case "request":
                    otherBids = otherBids.Where(s => s.status.Equals(true));
                    break;
                case "asset":
                    otherBids = otherBids.Where(s => s.status.Equals(false));
                    break;
                case "all":
                    otherBids = from m in OtherBids
                                select m;
                    break;
            }

            var myBids = from m in MyBids
                         select m;
            switch (sortby)
            {

                case "request":

                    myBids = myBids.Where(s => s.status.Equals(true));
                    break;
                case "asset":
                    myBids = myBids.Where(s => s.status.Equals(false));
                    break;
                case "all":
                    myBids = from m in MyBids
                             select m;
                    break;
            }

            List<Asset> list_asset = new List<Asset>();
            foreach (var a in assets)
            {
                list_asset.Add(a);
            }

            // gets all his assets 
            List<Asset> asset_list = new List<Asset>();
            foreach (var a in list_asset)
            {
                if (a.ownerID == userId)
                {
                    asset_list.Add(a);
                }
            }
            // All posts that you made INBOX
            List<Bid> inbox_list = new List<Bid>();
            foreach (var b in otherBids)
            {
                inbox_list.Add(b);

            }
            // All post that you bid on OUTBOX
            List<Bid> outbox_list = new List<Bid>();
            foreach (var item in myBids)
            {
                if (item.bidder == user.UserName)
                {
                    outbox_list.Add(item);
                }
            }
            UnitedBidViewModel completeBidModel = new UnitedBidViewModel();
            completeBidModel.assetModel = asset_list;
            completeBidModel.outbox = outbox_list;
            completeBidModel.inbox = inbox_list;
            await _context.SaveChangesAsync();
            return View(completeBidModel);

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Transcation([Bind("bidId,bidPrice,description,endDate,startDate,bidder")] Bid bid) {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userId = user?.Id;
                bid.chosen = true;
                await _context.SaveChangesAsync();
                RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Transaction", new { @Id = bid.bidId }); // new { @Id = asset_id });
                return redirectResult;
            }
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
