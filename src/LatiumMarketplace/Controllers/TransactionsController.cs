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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("assetID,addDate,description,location,name,ownerID,price,priceDaily,priceWeekly,priceMonthly,request,accessory")]  Transaction transaction)
       // {

           /* if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userId = user?.Id;
                DateTime today = DateTime.Now;
                transaction.transactionDate = today;
                transaction. = userId;
                asset.request = false;
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset);
        }*/




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
         }

    }
}
