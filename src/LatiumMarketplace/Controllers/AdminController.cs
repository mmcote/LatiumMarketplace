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
using Microsoft.AspNetCore.Http;

namespace LatiumMarketplace.Controllers
{
    /// <summary>
    /// Admin Controller handles all of the functioning of the admin panel.
    /// The admin panel handles mass messaging users, banning members, and handling requests.
    /// </summary>
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

        /// <summary>
        /// Main admin pannel, this shows a list of all users.
        /// </summary>
        [AllowAnonymous]
        public IActionResult Index()
        {
            var users = _context.User.Where(u => u.Email != User.Identity.Name);
            return View(users);
        }

        /// <summary>
        /// This API request will ban the user after given the users email.
        /// </summary>
        /// <param name="email"></param>
        [AllowAnonymous]
        public IActionResult BanMember(string email)
        {
            var user = _context.User.Single(u => u.Email == email);
            user.banned = true;
            _context.SaveChanges();

            Clients.Group(user.Email).CheckBan();
            return View(user);
        }

        /// <summary>
        /// This API request will ban the user after given the users email.
        /// </summary>
        /// <param name="email"></param>
        [AllowAnonymous]
        public IActionResult UnbanMember(string email)
        {
            var user = _context.User.Single(u => u.Email == email);
            user.banned = false;
            _context.SaveChanges();

            return View(user);
        }

        /// <summary>
        /// Send to all will send a message to all users, that have registered to the 
        /// website. Even to other admin.
        /// </summary>
        [AllowAnonymous]
        public async Task<IActionResult> SendToAll()
        {
            MessageThreadDTO messageThreadDTO = new MessageThreadDTO();
            return View(messageThreadDTO);
        }

        /// <summary>
        /// Send to all will send a message to all users, that have registered to the 
        /// website. Even to other admin.
        /// </summary>
        /// <param name="messageThreadDTO"></param>
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
                message = new Message(messageThreadDTO.Subject, messageThreadDTO.Body, false, false);
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
                    if (User.Identity.Name == message.messageThread.RecieverEmail)
                    {
                        recieverEmail = message.messageThread.SenderEmail;
                        message.messageThread.SenderUnreadMessageCount += 1;
                        message.SenderUnread = true;
                    }
                    else
                    {
                        recieverEmail = message.messageThread.RecieverEmail;
                        message.messageThread.RecieverUnreadMessageCount += 1;
                        message.RecieverUnread = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    messageRepo.AddMessage(message);
                    MessageThread messageThread = new MessageThread(messageThreadDTO.SenderId, messageThreadDTO.RecieverId);
                    messageThread.SenderEmail = _context.User.Single(u => u.Id == messageThreadDTO.SenderId).Email;
                    messageThread.RecieverEmail = _context.User.Single(u => u.Id == messageThreadDTO.RecieverId).Email;

                    messageThreadId = messageThread.id.ToString();
                    messageThread.messages.Add(message);
                    if (User.Identity.Name == messageThread.RecieverEmail)
                    {
                        recieverEmail = messageThread.SenderEmail;
                        messageThread.SenderUnreadMessageCount += 1;
                        message.SenderUnread = true;
                    }
                    else
                    {
                        recieverEmail = messageThread.RecieverEmail;
                        messageThread.RecieverUnreadMessageCount += 1;
                        message.RecieverUnread = true;
                    }

                    messageThread.LastUpdateDate = DateTime.Now;
                    messageThreadRepo.AddMessageThread(messageThread);
                }

                // This notification redirect URL should put the user to the discussion
                redirectURL = "/MessageThreads/Details/" + message.messageThread.id.ToString();
                notification = new Notification(message.Subject, message.Body, redirectURL);
                Clients.Group(recieverEmail).AddNotificationToQueue(notification);
            }
            _context.SaveChanges();

            foreach(ApplicationUser user in users)
            {
                Clients.Group(user.Email).UpdateOverallNotificationCount();
            }

            return RedirectToAction(nameof(AdminController.Index));
        }

        /// <summary>
        /// Send to all will send a message to all users, that have registered to the 
        /// website. Even to other admin.
        /// </summary>
        /// <param name="messageThreadDTO"></param>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SendMessageToUser([Bind("AssetId, Subject, Body, SenderId, RecieverId")] MessageThreadDTO messageThreadDTO)
        {
            messageThreadDTO.RecieverId = HttpContext.Request.Cookies["RecieverId"];
            var adminUser = _context.User.Single(u => u.Email == User.Identity.Name);
            messageThreadDTO.SenderId = adminUser.Id;
            messageThreadDTO.AssetId = 0;

            MessageRepository messageRepo = new MessageRepository(_context);
            MessageThreadRepository messageThreadRepo = new MessageThreadRepository(_context);

            // The reciever will always be the seller
            Message message;
            Notification notification;

            message = new Message(messageThreadDTO.Subject, messageThreadDTO.Body, false, false);
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
                message.messageThread.RecieverUnreadMessageCount += 1;
                message.RecieverUnread = true;
            }
            catch (InvalidOperationException)
            {
                messageRepo.AddMessage(message);
                MessageThread messageThread = new MessageThread(messageThreadDTO.SenderId, messageThreadDTO.RecieverId);
                messageThread.SenderEmail = _context.User.Single(u => u.Id == messageThreadDTO.SenderId).Email;
                messageThread.RecieverEmail = _context.User.Single(u => u.Id == messageThreadDTO.RecieverId).Email;

                messageThreadId = messageThread.id.ToString();
                messageThread.messages.Add(message);

                recieverEmail = messageThread.RecieverEmail;
                messageThread.RecieverUnreadMessageCount += 1;
                message.RecieverUnread = true;

                messageThread.LastUpdateDate = DateTime.Now;
                messageThreadRepo.AddMessageThread(messageThread);
            }

            // This notification redirect URL should put the user to the discussion
            redirectURL = "/MessageThreads/Details/" + message.messageThread.id.ToString();
            notification = new Notification(message.Subject, message.Body, redirectURL);
            Clients.Group(recieverEmail).AddNotificationToQueue(notification);
            _context.SaveChanges();

            Clients.Group(recieverEmail).UpdateOverallNotificationCount();

            return RedirectToAction(nameof(AdminController.Index));
        }

        public IActionResult MessageSentToAll()
        {
            return View();
        }

        public IActionResult SendMessageToUser(string id)
        {
            HttpContext.Response.Cookies.Append("RecieverId", id,
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                }
            );
            return View();
        }

        // GET: God mode for assets with featured iteam option to promote an item
        [AllowAnonymous]
        public async Task<IActionResult> AdminListings(string assetLocation, string searchString, string sortby, bool recent, bool accessory,bool featuredItem, bool featured)
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

            foreach(var item in assets)
            {
                if (featured == true)
                {
                    item.featuredItem.Equals(true);
                }
            }

            var assetLocatioinVM = new AssetLocation();
            assetLocatioinVM.locations = new SelectList(await locationQuery.Distinct().ToListAsync());
            assetLocatioinVM.assets = await assets.ToListAsync();
            return View(assetLocatioinVM);
        }

        // Get: Admin/FeaturedItems
        public  async Task<IActionResult> AddFeature(int? id)
        {
            var asset = _context.Asset.Single(s =>s.assetID == id );
            asset.featuredItem = true;
            await _context.SaveChangesAsync();
        
            return RedirectToAction("AdminListings");
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
        public IActionResult AddCategory(Category category)
        {
            var MyCategory = _context.Category;
            var categories = from m in MyCategory
                             select m;


            return View(categories);
        }
        //Create new make for site
        [AllowAnonymous]
        public IActionResult AddMake(Make make)
        {

            var MyMake = _context.Make;
            var Makes = from m in MyMake
                        select m;

            return View(Makes);
        }
        //Create new city for site
        [AllowAnonymous]
        public IActionResult AddCity(City city)
        {

            var MyCity = _context.City;
            var Cities = from m in MyCity
                         select m;

            return View(Cities);
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
        #region Helpers

        #endregion
    }
}
