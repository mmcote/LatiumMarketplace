using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.ManageViewModels;

namespace LatiumMarketplace.Models
{
    public class UserViewModel
    {

        public List<ApplicationUser> applicationModel { get; set; }
        public IndexViewModel indexModel { get; set; }
       
    }
}
