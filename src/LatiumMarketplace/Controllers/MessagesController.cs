using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models.MessageViewModels;
using Microsoft.AspNetCore.Http;

namespace LatiumMarketplace.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IMessageThreadRepository _messageThreadRepo;
        private IMessageRepository _messageRepo;
        public MessagesController(ApplicationDbContext context)
        {
            _messageRepo = new MessageRepository(context);
            _messageThreadRepo = new MessageThreadRepository(context);
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Message.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadId, Body, Subject")] Message message)
        {
            string messageThreadId  = HttpContext.Request.Cookies["threadId"];

            Guid messageThreadIdGuid = Guid.Parse(messageThreadId);
            MessageThread messageThread = _messageThreadRepo.GetMessageThreadByID(messageThreadIdGuid);
            messageThread.LastUpdateDate = DateTime.Now;
            message.messageThread = messageThread;

            if (ModelState.IsValid)
            {
                message.id = Guid.NewGuid();
                message.SendDate = DateTime.Now;
                _messageRepo.AddMessage(message);
                _messageRepo.Save();
                return RedirectToAction("Details", "MessageThreads", new { id = messageThreadId });
            }
            return View(message);
        }
    }
}
