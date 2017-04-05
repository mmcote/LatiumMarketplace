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
    /// <summary>
    /// BidsController
    /// </summary>
    public class BidsController : ApiHubController<Broadcaster>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BidsAPIController _BidsApiController;
        private readonly BidRepository _bidRepo;

        public BidsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConnectionManager connectionManager)
            : base(connectionManager)
        {
            _userManager = userManager;
            _context = context;
            _BidsApiController = new BidsAPIController(context, _bidRepo);
        }

        // GET: Bids
        /// <summary>
        /// Index for bids
        /// </summary>
        /// <returns>View list of bids</returns>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
            return RedirectToAction("MyBids");
            //return View(await _context.Bid.ToListAsync());
        }

        // GET: Bids/Details/5
        /// <summary>
        /// Bid controller for Details
        /// </summary>
        /// <param name="id">Bid ID</param>
        /// <returns>Returns Details of Bid</returns>
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
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

        // GET: Bids/Create
        /// <summary>
        /// Bid controller for create
        /// </summary>
        /// <returns>View for create</returns>
        public IActionResult Create()
        {
            return View();
        }


        // GET: Bids/Create
        // Bids for assets
        /// <summary>
        /// Bid controller for asset create
        /// Create bid for specific asset
        /// </summary>
        /// <param name="assetId">Asset Id</param>
        /// <returns>View of bid</returns>
        public IActionResult Create_Asset_Bid(int assetId)
        {
            Bid bid = new Bid();
            var Bid_asset = _context.Asset.Single(s => s.assetID == assetId);
            bid.asset = Bid_asset;

            DateTime current = DateTime.Now;
            if ((DateTime.Now - bid.asset.addDate).TotalDays > 0)
            {
                bid.startDate = DateTime.Now;
            }
            else
            {
                bid.startDate = bid.asset.addDate;
            }
            bid.endDate = bid.startDate.AddDays(1);

            return View(bid);
        }


        // POST: Bids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Bid Create POST
        /// </summary>
        /// <param name="bid">Input from form</param>
        /// <returns>View with created bid</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("bidId,bidPrice,description,endDate,startDate,bidder")] Bid bid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
            if (ModelState.IsValid)
            {
                string id = HttpContext.Request.Cookies["assetId"];
                int asset_id = Int32.Parse(id);
                Asset asset = _context.Asset.Single(a => a.assetID == asset_id);
                bid.asset = asset;
                bid.assetOwnerNotificationPending = true;
                bid.bidderNotificationPending = false;
                bid.asset_id_model = asset_id;
                bid.asset_name = asset.name;
                bid.status = asset.request;
                bid.chosen = false;
                var userId = user?.Id;
                var userName = user?.UserName;
                bid.bidder = userName;

                // Calculate Price
                if (bid.asset.price > (decimal)0.00)
                {
                    bid.bidPrice = bid.asset.price;
                }
                else
                {
                    double numDays = (bid.endDate - bid.startDate).TotalDays;
                    int month = 0, week = 0, remain = 0;
                    if (bid.asset.priceMonthly > (decimal)0.00)
                    {
                        month = (int)numDays / 30;
                        remain = (int)numDays % 30;
                        bid.bidPrice = (bid.asset.priceMonthly * month);
                    }
                    if (bid.asset.priceWeekly > (decimal)0.00)
                    {
                        // Monthly rate was available
                        if (remain > 0)
                        {
                            week = remain / 7;
                            remain = remain % 7;
                            bid.bidPrice = bid.bidPrice + (bid.asset.priceWeekly * week);
                        }
                        // Monthly rental not available
                        else if (bid.bidPrice == (decimal)0.00)
                        {
                            week = (int)numDays / 7;
                            remain = (int)numDays % 7;
                            bid.bidPrice = (bid.asset.priceWeekly * week);
                        }
                    }
                    if (bid.asset.priceDaily > (decimal)0.00)
                    {
                        if (remain > 0)
                        {
                            bid.bidPrice = bid.bidPrice + (bid.asset.priceDaily * remain);
                            remain = 0;
                        }
                        // Monthly and Weekly rental not available
                        else if (bid.bidPrice == (decimal)0.00)
                        {
                            bid.bidPrice = (bid.asset.priceDaily * (int)numDays);
                            remain = 0;
                        }
                    }
                    // Meaning that Daily Rate is not available remain is between 6 to 1
                    if (remain > 0)
                    {
                        if (bid.asset.priceWeekly > (decimal)0.00)
                        {

                            bid.bidPrice = bid.bidPrice + bid.asset.priceWeekly;
                        }
                        else
                        {
                            bid.bidPrice = bid.bidPrice + bid.asset.priceMonthly;
                        }
                    }
                }


                _context.Add(bid);
                await _context.SaveChangesAsync();

                // This notification redirect URL should put the user to the discussion
                string redirectURL = "/Bids/MyBids/";
                Notification notification = new Notification(bid.asset_name,
                    "There has been a new bid placed on your asset, " + bid.asset_name + ".", redirectURL);
                notification.type = 1;
                string notificationEmail = _context.User.Single(u => u.Id == bid.asset.ownerID).Email;
                Clients.Group(notificationEmail).AddNotificationToQueue(notification);
                Clients.Group(notificationEmail).UpdateOverallNotificationCount();
                //RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Bids", new { @Id = bid.bidId });

                return RedirectToAction("MyBids");
            }
            return View(bid);
        }

        //Listing of assets/requests belonging to a specific user
        /// <summary>
        /// List of bids associated with the user
        /// </summary>
        /// <param name="sortby">Enter paramters to sortby</param>
        /// <returns>View List of your bids </returns>
        [AllowAnonymous]
        public async Task<IActionResult> MyBids(string sortby)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
            var userId = user?.Id;
            var MyBids = _context.Bid.Where(s => s.bidder == user.UserName); // everything you bid on
            var OtherBids = _context.Bid.Where(s => s.asset.ownerID == userId); //shows only his assets that have bids on them
            var my_Assets = _context.Asset.Where(s => s.assetID != 0); // get all assets
            foreach(Bid bid in MyBids)
            {
                bid.bidderNotificationPending = false;
            }
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
        /// <summary>
        /// Bids that are placed on your asset
        /// </summary>
        /// <param name="sortby">Enter parameter to sortby</param>
        /// <returns>View list of bids that are placed on your asset</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Inbox(string sortby)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
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

        /// <summary>
        /// Accept a bid
        /// </summary>
        /// <param name="id">Bid ID of accpeted bid</param>
        /// <returns>Returns to index</returns>
        public async Task<IActionResult> Choose(int? id) {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
            var bid = _context.Bid.Single(s => s.bidId == id);
            bid.chosen = true;
            bid.assetOwnerNotificationPending = false;
            bid.bidderNotificationPending = true;

            //var listBid = _context.Bid.Where(s => s.asset.assetID == bid.asset.assetID);



            //Remove ALL bids if sale or request 
            //Remove the correlating Asset as well
            //if ((bid.asset.price > (decimal)0.00) || (bid.asset.request == true))
            //{
            //    foreach (var bidToRemove in listBid)
            //    {
            //        if (bidToRemove.bidId != id)
            //        {
            //            _context.Bid.Remove(bidToRemove);
            //        }
            //    }
            //    _context.Asset.Remove(bid.asset);
            //}
            ////Remove bid of rental
            //else
            //{
            //    foreach (var bidToRemove in listBid)
            //    {
            //        if ((bidToRemove.startDate < bid.endDate) && (bidToRemove.bidId != id))
            //        {
            //            _context.Bid.Remove(bidToRemove);
            //        }
            //    }
            //}

            await _context.SaveChangesAsync();


            // This notification redirect URL should put the user to the discussion
            string redirectURL = "/Bids/MyBids/";
            Notification notification = new Notification(bid.asset_name,
                "Your bid has been choose for " + bid.asset_name + ".", redirectURL);
            notification.type = 1;
            Clients.Group(bid.bidder).AddNotificationToQueue(notification);
            Clients.Group(bid.bidder).UpdateOverallNotificationCount();

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Transaction([Bind("bidId,bidPrice,description,endDate,startDate,bidder")] Bid bid) {

        //    //Find all bids for the given asset
        //    var notChoosenBids = _context.Bid.Where(s => s.asset.assetID == bid.asset.assetID);
        //    if (ModelState.IsValid)
        //    {


        //        var user = await _userManager.GetUserAsync(HttpContext.User);
        //        var userId = user?.Id;
        //        bid.chosen = true;
        //        await _context.SaveChangesAsync();

        //        //Find bids not chosen and remove
        //        notChoosenBids = notChoosenBids.Where(s => s.chosen == false);
        //        foreach (var bidToRemove in notChoosenBids)
        //        {
        //            _context.Remove(bidToRemove);
        //        }

        //        RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Transaction", new { @Id = bid.bidId }); // new { @Id = asset_id });
        //        return redirectResult;
        //    }

        //    return View();
        //}



        // GET: Bids/Delete/5
        /// <summary>
        /// Bid controller for Delete bid 
        /// </summary>
        /// <param name="id">Bid ID of bid to remove</param>
        /// <returns>Return view of confirmation delete</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }

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
        /// <summary>
        /// Bid controller POST of delete bid
        /// </summary>
        /// <param name="id">Bid id of delete bids</param>
        /// <returns>View Index with bid deleted</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }

            var bid = await _context.Bid.SingleOrDefaultAsync(m => m.bidId == id);
            _context.Bid.Remove(bid);
            await _context.SaveChangesAsync();

            // This notification redirect URL should put the user to the discussion
            string redirectURL = "/Bids/MyBids/";
            Notification notification = new Notification(bid.asset_name,
                "Your bid has been rejected for " + bid.asset_name + ".", redirectURL);
            notification.type = 1;
            Clients.Group(bid.bidder).AddNotificationToQueue(notification);

            return RedirectToAction("MyBids");
        }

        private bool BidExists(int id)
        {
            return _context.Bid.Any(e => e.bidId == id);
        }
    }
}
