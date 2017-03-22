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

namespace LatiumMarketplace.Controllers
{
    //redirect all HTTP GET requests to HTTPS GET and will reject all HTTP POSTs
    [RequireHttps]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
                message = new Message(messageThreadDTO.Subject, messageThreadDTO.Body);

                try
                {
                    var messageThreadRetrieved = _context.MessageThread.Single(m => m.SenderId == messageThreadDTO.SenderId && m.RecieverId == messageThreadDTO.RecieverId);
                    message.messageThread = messageThreadRetrieved;
                    message.messageThread.LastUpdateDate = DateTime.Now;
                    messageRepo.AddMessage(message);
                }
                catch (InvalidOperationException)
                {
                    messageRepo.AddMessage(message);
                    MessageThread messageThread = new MessageThread(messageThreadDTO.SenderId, messageThreadDTO.RecieverId);
                    messageThread.messages.Add(message);

                    messageThread.LastUpdateDate = DateTime.Now;

                    messageThread.SenderEmail = _context.User.Single(u => u.Id == messageThreadDTO.SenderId).Email;
                    messageThread.RecieverEmail = _context.User.Single(u => u.Id == messageThreadDTO.RecieverId).Email;

                    messageThreadRepo.AddMessageThread(messageThread);
                }
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


            switch (sortby)
            {

                case "request":
                    if (accessory == true)
                    {
                        assets = assets.Where(s => s.accessory != null);
                    }
                    assets = assets.Where(s => s.request.Equals(true));
                    break;
                case "asset":
                    if (accessory == true)
                    {
                        assets = assets.Where(s => s.accessory != null);
                    }
                    assets = assets.Where(s => s.request.Equals(false));
                    break;
                case "all":
                    assets = from m in Myassets
                             select m;
                    if (accessory == true)
                    {
                        assets = assets.Where(s => s.accessory != null);
                    }
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

        #endregion
    }
}
