using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Asset.Any())
            {
                return;   // DB has been seeded
            }

            var assets = new Asset[]
            {
            new Asset{addDate = DateTime.Parse("2005-09-01"),description="This will only work above 15 degree C", location="Edmonton, Alberta", name="Bobcat", ownerID="TestCode233", price=50000, priceDaily=180, priceMonthly=2500,priceWeekly=890, accessory="With Cover", request=false},
             new Asset{addDate = DateTime.Parse("2015-09-01"),description="This will only work above 20 degree C", location="Calgary, Alberta", name="Tow Truck", ownerID="TestCode233", price=110000, priceDaily=400, priceMonthly=4000,priceWeekly=2800, accessory="Winter tires on with stud", request=false},
             new Asset{addDate = DateTime.Parse("2017-01-01"),description="This will work above - 15 degree C", location="Edmonton, Alberta", name="Semi-16wheel", ownerID="TestCode233", accessory= null, request=true},
             new Asset{addDate = DateTime.Parse("2017-03-01"),description="This will work above - 30 degree C", location="St.albert, Alberta", name="Semi-18wheel", ownerID="TestCode233", accessory= "Comes with self driving system", request=true},
            };
            foreach (Asset s in assets)
            {
                context.Asset.Add(s);
            }
            context.SaveChanges();
        }
    }
}
