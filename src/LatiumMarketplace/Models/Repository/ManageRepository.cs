using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.ManageViewModels;
using LatiumMarketplace.Models.Context;
using LatiumMarketplace.Models.IRepository;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Models.Repository
{
    public class ManageRepository : IManageRepository
    {
        private readonly ManageContext _context;

        public void Update(ApplicationUser User)
        {
            _context.Update(User);
            _context.SaveChanges();
        }
    }
}
