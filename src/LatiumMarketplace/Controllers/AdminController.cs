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

        // GET: Assets
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
                    var messageThreadRetrieved = _context.MessageThread.Single(m => m.SenderId == messageThreadDTO.SenderId);
                    message.messageThread = messageThreadRetrieved;
                    messageRepo.AddMessage(message);
                }
                catch (InvalidOperationException)
                {
                    messageRepo.AddMessage(message);
                    MessageThread messageThread = new MessageThread(messageThreadDTO.SenderId, messageThreadDTO.RecieverId);
                    messageThread.messages.Add(message);
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
