using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.MessageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace LatiumMarketplace.Controllers
{
    /// <summary>
    /// MessageThreadsController handles the processing of pages that deal with showing message threads and messages.
    /// </summary>
    [RequireHttps]
    public class MessageThreadsController : ApiHubController<Broadcaster>
    {
        private readonly ApplicationDbContext _context;
        private MessageThreadAPIController _messageThreadApiController;
        private MessagesAPIController _messageApiController;
        private UserManager<ApplicationUser> _userManager;

        public MessageThreadsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConnectionManager connectionManager)
            : base(connectionManager)
        {
            _context = context;
            _userManager = userManager;
            _messageThreadApiController = new MessageThreadAPIController(context, connectionManager);
            _messageApiController = new MessagesAPIController(new MessageRepository(context), new MessageThreadRepository(context), connectionManager);
        }

        // GET: MessageThreads
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            OkObjectResult wrappedMessageThreads = (OkObjectResult) _messageThreadApiController.Get(userId);
            var messageThreads = (IEnumerable<MessageThread>) wrappedMessageThreads.Value;
            messageThreads = messageThreads.OrderBy(m => m.LastUpdateDate).Reverse();
            return View(messageThreads);
        }

        // This call should return a list of the messages within the thread sorted by date
        // GET: MessageThreads/Details/
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string guid = id.ToString();
            OkObjectResult messageThreadWrapped = (OkObjectResult) _messageThreadApiController.GetMessageThread(guid);
            MessageThread messageThread = (MessageThread) messageThreadWrapped.Value;
            IEnumerable<Message> threadMessages = messageThread.messages;
            threadMessages = threadMessages.OrderBy(m => m.SendDate).Reverse();
            if (threadMessages == null)
            {
                return NotFound();
            }
            MessageDetailsView messageDetailsView;
            ApplicationUser user = _context.User.Single(u => u.Email == User.Identity.Name);
            if (user.Id == messageThread.SenderId)
            {
                string email = messageThread.RecieverEmail;
                messageDetailsView = new MessageDetailsView(guid, threadMessages, email, true, messageThread.asset);
            }
            else
            {
                string email = messageThread.SenderEmail;
                messageDetailsView = new MessageDetailsView(guid, threadMessages, email, false, messageThread.asset);
            }

            HttpContext.Response.Cookies.Append(
                "threadId",
                guid,
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                }
            );

            return View(messageDetailsView);
        }

        // GET: MessageThreads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageThreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subject, Body")] MessageThreadDTO messageThreadDTO)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Redirect("/Account/Login");
            }
            string assetIdString = HttpContext.Request.Cookies["assetId"];
            int assetId = int.Parse(assetIdString);
            string userId = await _userManager.GetUserIdAsync(user);
            messageThreadDTO.RecieverId = HttpContext.Request.Cookies["assetOwnerId"];
            messageThreadDTO.SenderId = userId;
            messageThreadDTO.IsSender = true;
            messageThreadDTO.AssetId = assetId;
            _messageThreadApiController.Post(messageThreadDTO);

            return RedirectToAction("Index");
        }

        // GET: MessageThreads/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

            var messageThread = await _context.MessageThread.SingleOrDefaultAsync(m => m.id == id);
            if (messageThread == null)
            {
                return NotFound();
            }

            return View(messageThread);
        }

        // POST: MessageThreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _messageThreadApiController.Delete(id.ToString());
            return RedirectToAction("Index");
        }

        private bool MessageThreadExists(Guid id)
        {
            return _context.MessageThread.Any(e => e.id == id);
        }
    }
}
