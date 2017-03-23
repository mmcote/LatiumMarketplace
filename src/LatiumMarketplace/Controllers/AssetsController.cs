using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models.AssetViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LatiumMarketplace.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace LatiumMarketplace.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _env;



        public AssetsController(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager, IHostingEnvironment env
        )
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        //Listing of assets/requests belonging to a specific user
        [AllowAnonymous]
        public async Task<IActionResult> MyListings(string assetLocation, string searchString, string sortby, bool recent, bool accessory)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user?.Id;
            var Myassets = _context.Asset.Where(s => s.ownerName == userId);

            IQueryable<string> locationQuery = from m in Myassets
                                               orderby m.Address
                                               select m.Address;

            var assets = from m in Myassets
                         select m;

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

        /*
        // GET: Assets
        [AllowAnonymous]
        public async Task<IActionResult> Index(string assetLocation, string searchString, string sortby, bool recent, bool accessory)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> locationQuery = from m in _context.Asset
                                               orderby m.location
                                               select m.location;

            var assets = from m in _context.Asset
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
                    assets = from m in _context.Asset
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
                assets = assets.Where(x => x.location == assetLocation);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(x => x.name.Contains(searchString));
            }

            var assetLocatioinVM = new AssetLocation();
            assetLocatioinVM.locations = new SelectList(await locationQuery.Distinct().ToListAsync());
            assetLocatioinVM.assets = await assets.ToListAsync();
            return View(assetLocatioinVM);
            //return View(await assets.ToListAsync());
        }
        */

        /*============================ */

        // GET: Assets
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? id, int? assetId)
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
            
            if (id != null)
            {
                ViewData["AssetID"] = id.Value;
                Asset asset = viewModel.Assets.Where(
                    a => a.assetID == id.Value).Single();
                viewModel.Categories = asset.AssetCategories.Select(s => s.Category);
            }

            return View(viewModel); 
        }






        /*============================= */










        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.assetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            HttpContext.Response.Cookies.Append("assetId", id.ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                }
            );
            HttpContext.Response.Cookies.Append("assetOwnerId", asset.ownerName.ToString(),
                new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false
                }
            );

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            // Populate asset categories
            SetCategoryViewBag();
            // Populate asset makes
            SetMakeViewBag();
            // Populate cities
            SetCityViewBag();

            return View();
        }

        // GET: Assets/CreateReq
        // Returns view for creating request
        public IActionResult CreateReq()
        {
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("assetID,addDate,description,Address,name,ownerName,price,priceDaily,priceWeekly,priceMonthly,request,accessory,AssetCategories")] Asset asset)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userName = user?.UserName;
                DateTime today = DateTime.Now;
                asset.addDate = today;
                asset.ownerName = userName;
                asset.request = false;

                // Assign make to asset
                var myMakeId = HttpContext.Request.Form["Makes"];
                var myMakeIdNumVal = int.Parse(myMakeId);
                asset.MakeId = myMakeIdNumVal;

                // Assign a city to the asset
                var myCityId = HttpContext.Request.Form["Cities"];
                var myCityIdNumVal = int.Parse(myCityId);
                asset.CityId = myCityIdNumVal;

                /* Add Images */
                // Get images from form
                var uploadedFiles = HttpContext.Request.Form.Files;
                // Get the wwwroot folder
                var webRootPath = _env.WebRootPath;
                // Set assets image folder
                var uploadsPath = Path.Combine(webRootPath, "images\\uploads\\assets");

                // Create Image Gallery to hold images only when there is
                // at least one image uploaded
                ImageGallery ImageGallery;
                int ImageGalleryId = -1;

                if (uploadedFiles.Count > 0)
                {
                    ImageGallery = new ImageGallery();
                    ImageGallery.Title = "My cool gallery";
                    // Add Image gallery to DB
                    _context.Add(ImageGallery);
                    await _context.SaveChangesAsync();
                    //Get Id of recently added Image Gallery
                    ImageGalleryId = ImageGallery.ImageGalleryId;
                }

                foreach (var uploadedFile in uploadedFiles)
                {
                    if (uploadedFile != null && uploadedFile.Length > 0)
                    {
                        var file = uploadedFile;
                        if (file.Length > 0)
                        {
                            // 1) Add image to DB
                            Image Image = new Image();
                            Image.ImageGalleryId = ImageGalleryId;
                            Image.FileLink = Path.Combine("images/uploads/assets/", file.FileName);
                            _context.Add(Image);
                            await _context.SaveChangesAsync();

                            // 2) Get Id or Guid of recently added image from DB
                            //int ImageId = Image.ImageId;
                            Guid ImageGuid = Image.ImageGuid; // Better

                            // 3) Save image to disk with Guid
                            var fileName = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition).FileName.Trim('"');
                            // 3.1) Get File extension from file
                            string fileExtesion = Path.GetExtension(fileName);
                            // 3.2) Change file name to Guid
                            fileName = ImageGuid + fileExtesion;
                            Console.WriteLine(fileName);
                            // 3.3) Save image to disk with new file name
                            using (var fileStream = new FileStream(Path.Combine(uploadsPath, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            // 4) Update FileLink in DB 
                            Image.FileLink = Path.Combine("images/uploads/assets/", fileName);
                            await _context.SaveChangesAsync();

                        }
                    }
                }

                // Attach Gallery to Asset if a new gallery is created
                if (ImageGalleryId != -1)
                {
                    asset.ImageGalleryId = ImageGalleryId;
                }

                // Save asset to DB
                _context.Add(asset);
                await _context.SaveChangesAsync();

                // Get the category from select form
                var myCategoryId = HttpContext.Request.Form["AssetCategories"];
                var myCategoryIdNumVal = int.Parse(myCategoryId);

                // Assign a category to the asset
                AssetCategory AssetCategory = new AssetCategory();
                AssetCategory.AssetId = asset.assetID;
                AssetCategory.CategoryId = myCategoryIdNumVal;

                _context.AssetCategory.Add(AssetCategory);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            SetCategoryViewBag(asset.AssetCategories);
            SetMakeViewBag();
            SetCityViewBag();
            return View(asset);
        }

        // POST: Assets/CreateReq
        // Used for creating requests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReq([Bind("assetID,addDate,description,Address,name,ownerName,price,priceDaily,priceWeekly,priceMonthly,request,accessory")] Asset asset)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var userName = user?.UserName;
                DateTime today = DateTime.Now;
                asset.addDate = today;
                asset.ownerName = userName;
                asset.request = true;
                asset.price = 0;
                asset.priceDaily = 0;
                asset.priceWeekly = 0;
                asset.priceMonthly = 0;
                asset.MakeId = 1;
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userName = user?.UserName;
            var Myassets = _context.Asset.Where(s => s.ownerName == userName);
            if (id == null)
            {
                return NotFound();
            }

            var asset = await Myassets.SingleOrDefaultAsync(m => m.assetID == id);
            if (asset == null)
            {
                return NotFound();
            }
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("assetID,addDate,description,Address,name,ownerName,pricep,riceDaily,priceWeekly,priceMonthly,request,accessory")] Asset asset)
        {
            if (id != asset.assetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.assetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.assetID == id);
            _context.Asset.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.assetID == id);
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
    }
}
