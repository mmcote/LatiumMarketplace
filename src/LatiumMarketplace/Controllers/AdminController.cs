using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.AccountViewModels;
using LatiumMarketplace.Services;
using LatiumMarketplace.Data;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using LatiumMarketplace.Models.MessageViewModels;
using LatiumMarketplace.Models.AssetViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using LatiumMarketplace.Hubs;

namespace LatiumMarketplace.Controllers
{
    //redirect all HTTP GET requests to HTTPS GET and will reject all HTTP POSTs
    [RequireHttps]
    [Authorize]
    public class AdminController : ApiHubController<Broadcaster>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConnectionManager connectionManager)
            : base(connectionManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var users = _context.User.Where(u => u.Email != User.Identity.Name);
            return View(users);
        }

        [AllowAnonymous]
        public async Task<IActionResult> BanMember(string email)
        {
            var user = _context.User.Single(u => u.Email == email);
            user.banned = true;
            _context.SaveChanges();

            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> UnbanMember(string email)
        {
            var user = _context.User.Single(u => u.Email == email);
            user.banned = false;
            _context.SaveChanges();

            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SendToAll()
        {
            MessageThreadDTO messageThreadDTO = new MessageThreadDTO();
            return View(messageThreadDTO);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendToAll([Bind("Subject, Body")] MessageThreadDTO messageThreadDTO)
        {
            var adminUser = _context.User.Single(u => u.Email == User.Identity.Name);
            messageThreadDTO.SenderId = adminUser.Id;
            messageThreadDTO.AssetId = 0;

            MessageRepository messageRepo = new MessageRepository(_context);
            MessageThreadRepository messageThreadRepo = new MessageThreadRepository(_context);
            
            var users = _context.User.Where(u => u.Email != User.Identity.Name);
            foreach (ApplicationUser user in users)
            {
                messageThreadDTO.RecieverId = user.Id;

                // The reciever will always be the seller
                Message message;
                Notification notification;
                message = new Message(messageThreadDTO.Subject, messageThreadDTO.Body);
                string messageThreadId;
                string recieverEmail;
                string redirectURL;
                try
                {
                    var messageThreadRetrieved = _context.MessageThread.Single(m => m.SenderId == messageThreadDTO.SenderId && m.RecieverId == messageThreadDTO.RecieverId);
                    message.messageThread = messageThreadRetrieved;
                    message.messageThread.LastUpdateDate = DateTime.Now;
                    messageRepo.AddMessage(message);
                    messageThreadId = message.messageThread.id.ToString();
                    recieverEmail = message.messageThread.RecieverEmail;
                }
                catch (InvalidOperationException)
                {
                    messageRepo.AddMessage(message);
                    MessageThread messageThread = new MessageThread(messageThreadDTO.SenderId, messageThreadDTO.RecieverId);
                    messageThreadId = messageThread.id.ToString();
                    messageThread.messages.Add(message);
                    recieverEmail = messageThread.RecieverEmail;

                    messageThread.LastUpdateDate = DateTime.Now;

                    messageThread.SenderEmail = _context.User.Single(u => u.Id == messageThreadDTO.SenderId).Email;
                    messageThread.RecieverEmail = _context.User.Single(u => u.Id == messageThreadDTO.RecieverId).Email;

                    messageThreadRepo.AddMessageThread(messageThread);
                }

                // This notification redirect URL should put the user to the discussion
                redirectURL = "/MessageThreads/Details/" + message.messageThread.id.ToString();
                notification = new Notification(message.Subject, message.Body, redirectURL);
                Clients.Group(recieverEmail).AddNotificationToQueue(notification);
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(AdminController.Index));
        }

        public async Task<IActionResult> MessageSentToAll()
        {
            return View();
        }

        // GET: God mode for assets with featured iteam option to promote an item
        [AllowAnonymous]
        public async Task<IActionResult> AdminListings(string assetLocation, string searchString, string sortby, bool recent, bool accessory,bool featuredItem)
        {
            var Myassets = _context.Asset;

            IQueryable<string> locationQuery = from m in Myassets
                                               orderby m.Address
                                               select m.Address;

            var assets = from m in Myassets
                         select m;

            if (featuredItem == true)
            {
                assets = assets.Where(s => s.featuredItem == true);
            }
            if (accessory == true)
            {
                assets = assets.Where(s => s.accessory != null);
            }
            switch (sortby)
            {

                case "request":
                    assets = assets.Where(s => s.request.Equals(true));
                    break;
                case "asset":
                    assets = assets.Where(s => s.request.Equals(false));
                    break;
                case "all":
                    assets = from m in assets
                             select m;
                    break;
            }

            if (recent == true)
            {
                assets = assets.OrderByDescending(s => s.addDate);
            }
            if (!String.IsNullOrEmpty(assetLocation))
            {
                assets = assets.Where(x => x.Address == assetLocation);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(x => x.name.Contains(searchString));
            }

            var assetLocatioinVM = new AssetLocation();
            assetLocatioinVM.locations = new SelectList(await locationQuery.Distinct().ToListAsync());
            assetLocatioinVM.assets = await assets.ToListAsync();
            return View(assetLocatioinVM);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> AdminEdit(int? id)
        {
            var Myassets = _context.Asset;
            if (id == null)
            {
                return NotFound();
            }

            var asset = await Myassets.SingleOrDefaultAsync(m => m.assetID == id);
            if (asset == null)
            {
                return NotFound();
            }
            // Populate asset categories
            SetCategoryViewBag();
            // Populate asset makes
            SetMakeViewBag();
            // Populate cities
            SetCityViewBag();
            return View(asset);

        }

        // POST: Assets/Edit for admin with featured item
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminEdit(int id, [Bind("assetID,addDate,description,Address,name,ownerID,pricep,riceDaily,priceWeekly,priceMonthly,request,accessory,featuredItem")] Asset asset,bool featuredItem)
        {
            var viewModel = new AssetIndexData();
            viewModel.Assets = await _context.Asset
                .Include(a => a.AssetCategories)
                    .ThenInclude(a => a.Category)
                .Include(a => a.ImageGallery)
                    .ThenInclude(a => a.Images)
                .AsNoTracking()
                .OrderBy(a => a.addDate)
                .ToListAsync();
            if (id != asset.assetID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if(featuredItem == true)
                    {
                        asset.featuredItem = true;
                    }
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.assetID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AdminListings");
            }
            return View(asset);
        }
        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.assetID == id);
        }
        #region Helpers

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        //Create new category for site
        [AllowAnonymous]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var MyCategory = _context.Category;
            var categories = from m in MyCategory
                             select m;
            if (ModelState.IsValid)
            {
               category.CategoryName = HttpContext.Request.Form["MyCategory"].ToString();
                _context.Add(category);
                await _context.SaveChangesAsync();

                return RedirectToAction("AddCategory");
            }

            //SetCategoryViewBag();
            return View(categories);
        }
        //Create new make for site
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMake(Make make)
        {

            if (ModelState.IsValid)
            {
                // Add make to make table
                var myMake = HttpContext.Request.Form["Makes"];
                make.Name = myMake;

                // Save Make to DB
                _context.Add(make);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            SetMakeViewBag();
            return View(make);
        }
        //Create new city for site
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(City city)
        {

            if (ModelState.IsValid)
            {
                // Assign a city to the asset
                var myCity = HttpContext.Request.Form["Cities"];
                city.Name = myCity;

                // Save City to DB
                _context.Add(city);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            SetCityViewBag();
            return View(city);
        }
        // Get all categories from the database
        private void SetCategoryViewBag(ICollection<AssetCategory> AssetCategories = null)
        {

            if (AssetCategories == null)

                ViewBag.AssetCategories = new SelectList(_context.Category, "CategoryId", "CategoryName");

            else
                ViewBag.AssetCategories = new SelectList(_context.Category.AsEnumerable(), "CategoryId", "CategoryName", AssetCategories);
        }

        // Get all the city from the database
        private void SetCityViewBag(ICollection<AssetCategory> Cities = null)
        {

            if (Cities == null)

                ViewBag.Cities = new SelectList(_context.City, "CityId", "Name");

            else
                ViewBag.Cities = new SelectList(_context.City.AsEnumerable(), "CityId", "Name", Cities);
        }

        // Get all the makes from the database
        private void SetMakeViewBag(ICollection<Make> Makes = null)
        {

            if (Makes == null)

                ViewBag.Makes = new SelectList(_context.Make, "MakeId", "Name");

            else
                ViewBag.Makes = new SelectList(_context.Make.AsEnumerable(), "MakeId", "Name", Makes);
        }
        #endregion
    }
}
