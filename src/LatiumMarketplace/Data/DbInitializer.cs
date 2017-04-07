using System;
using System.Linq;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureCreated();

                // Look for any students.
                if (context.Asset.Any() || context.City.Any() || context.Category.Any() || context.Make.Any() || context.Feature.Any())
                {
                    return;   // DB has been seeded
                }

                var assets = new Asset[]
                {
                //new Asset{addDate = DateTime.Parse("2005-09-01"),description="This will only work above 15 degree C", location="Edmonton, Alberta", name="Bobcat", ownerID="TestCode233", price=50000, priceDaily=180, priceMonthly=2500,priceWeekly=890, accessory="With Cover", request=false},
                 //new Asset{addDate = DateTime.Parse("2015-09-01"),description="This will only work above 20 degree C", location="Calgary, Alberta", name="Tow Truck", ownerID="TestCode233", price=110000, priceDaily=400, priceMonthly=4000,priceWeekly=2800, accessory="Winter tires on with stud", request=false},
                 //new Asset{addDate = DateTime.Parse("2017-01-01"),description="This will work above - 15 degree C", location="Edmonton, Alberta", name="Semi-16wheel", ownerID="TestCode233", accessory= null, request=true},
                 //new Asset{addDate = DateTime.Parse("2017-03-01"),description="This will work above - 30 degree C", location="St.albert, Alberta", name="Semi-18wheel", ownerID="TestCode233", accessory= "Comes with self driving system", request=true},
                  //new Asset{addDate = DateTime.Parse("2005-09-01"),description="This will only work above 15 degree C", location="Edmonton, Alberta", name="Bobcat", ownerID="TestCode233", price=50000, priceDaily=180, priceMonthly=2500,priceWeekly=890, accessory="With Cover", request=false},
                };

                var category = new Category[]
                {
                    new Category {CategoryName = "Car" },
                    new Category { CategoryName = "Truck" },
                    new Category { CategoryName = "Bus" },
                    new Category { CategoryName = "Other" },
                     new Category {CategoryName = "Coupe", ParentCategoryId = 1},
                    new Category { CategoryName = "Seadan", ParentCategoryId = 1},
                    new Category { CategoryName = "16-Passenger", ParentCategoryId = 2},
                    new Category { CategoryName = "1-level", ParentCategoryId = 3},
                    new Category { CategoryName = "2-level",ParentCategoryId = 3 }
                };

                var city = new City[]
                {
                    new City {Name = "Edmonton"},
                    new City {Name = "Calgary"},
                    new City {Name = "Vancouver"},
            };
                var make = new Make[]
               {
                    new Make {Name = "BMW"},
                    new Make {Name = "FORD"},
                    new Make {Name = "CHEVY"},
                    new Make {Name = "Other"}
           };
                var feature = new Feature[]
                {
                    new Feature {FeatureName = "Year" },
                    new Feature { FeatureName = "Cab" },
                    new Feature { FeatureName = "Number of Seats" },
                    new Feature { FeatureName = "Odometer" },
                    new Feature { FeatureName = "Engine Hours" },
                    new Feature { FeatureName = "Number of Axels" },
                    new Feature { FeatureName = "Fuel Tank Capicity" },
                    new Feature { FeatureName = "Four Wheel Drive", ShortDescription = "Yes or No" },
                };

                foreach (Asset s in assets)
                {
                    context.Asset.Add(s);
                }

                foreach (Category s in category)
                {
                    context.Category.Add(s);
                }
                foreach (City s in city)
                {
                    context.City.Add(s);
                }
                foreach (Make s in make)
                {
                    context.Make.Add(s);
                }
                foreach (Feature f in feature)
                {
                    context.Feature.Add(f);
                }
                context.SaveChanges();
            }
            catch { }
        }
    }
}