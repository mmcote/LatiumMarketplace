using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Models.TransactionViewModels;
using LatiumMarketplace.Models.BidViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LatiumMarketplace.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transaction.ToListAsync());
        }


        // POST: Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("transactionId,assetName,poster,bidder,price,transactionDate,start,end")]  Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                string id = HttpContext.Request.Cookies["bidId"];
                int bid_id = Int32.Parse(id);
                Bid bid = _context.Bid.Single(a => a.bidId == bid_id);

                // the dates will be different if an asset or request
               // if (bid.request == true)
                var user = await _userManager.GetUserAsync(HttpContext.User); // user is the person who placed the bid
                var bidWinner = user?.UserName;
                DateTime today = DateTime.Now;
                transaction.transactionDate = today;
                transaction.assetName = bid.asset.name;
                transaction.poster = bid.asset.ownerName;
                transaction.bidder = bidWinner;
                transaction.start = bid.startDate;
                transaction.end = bid.endDate;
                //asset.request = false;
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
/*
        // GET: Bids/Details/5
        public async Task<IActionResult> Details(Guid? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var trans = await _context.Transaction.SingleOrDefaultAsync(m => m.transactionId == id);
             if (trans == null)
             {
                 return NotFound();
             }

             return View(trans);
         }*/

    }
}
