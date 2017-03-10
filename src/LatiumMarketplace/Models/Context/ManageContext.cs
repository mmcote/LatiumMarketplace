using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LatiumMarketplace.Models.Context
{
    public class ManageContext : DbContext
    {
        public ManageContext(DbContextOptions<ManageContext> options)
            : base(options)
        {
        }

        public DbSet<ManageViewModels.ProfileViewModel> Profile { get; set; }
    }
}
