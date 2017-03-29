using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models.RequestViewModel;

namespace LatiumMarketplace.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Request.Include(r => r.AccessoryList).Include(r => r.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request.SingleOrDefaultAsync(m => m.requestID == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["AccessoryListId"] = new SelectList(_context.AccessoryList, "AccessoryListId", "AccessoryListId");
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CityId");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("requestID,AccessoryListId,CityId,accessory,address,description,duration,name,ownerID,ownerName")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AccessoryListId"] = new SelectList(_context.AccessoryList, "AccessoryListId", "AccessoryListId", request.AccessoryListId);
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CityId", request.CityId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request.SingleOrDefaultAsync(m => m.requestID == id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["AccessoryListId"] = new SelectList(_context.AccessoryList, "AccessoryListId", "AccessoryListId", request.AccessoryListId);
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CityId", request.CityId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("requestID,AccessoryListId,CityId,accessory,address,description,duration,name,ownerID,ownerName")] Request request)
        {
            if (id != request.requestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.requestID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AccessoryListId"] = new SelectList(_context.AccessoryList, "AccessoryListId", "AccessoryListId", request.AccessoryListId);
            ViewData["CityId"] = new SelectList(_context.City, "CityId", "CityId", request.CityId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request.SingleOrDefaultAsync(m => m.requestID == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Request.SingleOrDefaultAsync(m => m.requestID == id);
            _context.Request.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.requestID == id);
        }
    }
}
