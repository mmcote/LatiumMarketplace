using LatiumMarketplace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Data
{
    public class AdministratorSeedData
    {
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private static string AdminEmail = "test@test.com";
        private static string AdminPassword = "abc123ABC123&";
        
        public AdministratorSeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync(AdminEmail) == null)
            {
                ApplicationUser administrator = new ApplicationUser()
                {
                    UserName = AdminEmail,
                    Email = AdminEmail
                };

                var result01 = await _userManager.CreateAsync(administrator, AdminPassword);

                var user = _context.User.Single(u => u.UserName == administrator.UserName);
                user.EmailConfirmed = true;
                var result02 = _context.SaveChanges();
                var resutl03 = await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                
                IdentityResult result = await _userManager.AddToRoleAsync(administrator, "Administrator");
            }
        }
    }
}
