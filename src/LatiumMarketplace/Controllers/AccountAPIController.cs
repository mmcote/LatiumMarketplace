﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LatiumMarketplace.Controllers
{
    /// <summary>
    /// AccountAPIController is used to check on specific properties of the user
    /// while doing this purely through a simple ajax requests.
    /// </summary>
    [Route("api/[controller]")]
    public class AccountAPIController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountAPIController(ApplicationDbContext context,
                        UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// CheckIfBanned is used to check whether a certain user is banned. This is done
        /// purely by passing in the email of the user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        // POST: /Account/LogOff
        [HttpPost("{id}")]
        [Route("api/AccountAPI/CheckIfBanned")]
        public async Task<IActionResult> CheckIfBanned([FromBody] string email)
        {
            var user = _context.User.Single(u => u.Email == email);
            if (user.banned)
            {
                await _signInManager.SignOutAsync();
            }
            return new OkObjectResult(user.banned);
        }
    }
}
