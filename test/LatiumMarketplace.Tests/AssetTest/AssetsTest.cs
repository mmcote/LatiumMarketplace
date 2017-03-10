using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Controllers;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using LatiumMarketplace.Models;
using Microsoft.AspNetCore.Mvc;
using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Data;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Models.IRepository;
using LatiumMarketplace.Models.Repository;


namespace LatiumMarketplace.Tests.AssetTest
{
    public class AssetsTest
    {
        [Fact]
        public void testAddAsset()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: "Add_asset_from_database")
    .Options;

            // Arrange
            var mockDB = new Mock<ApplicationDbContext>();
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            var controller = new AssetsController(mockDB.Object, mockUser.Object);

            //Mock Asset
            int assetId = 1;
            string name = "Bob Cat";
            string description = "Fast and strong";
            DateTime addDate = DateTime.Now;
            decimal price = 5;
            decimal priceDaily = 5;
            decimal priceWeekly = 5;
            decimal priceMonthly = 5;
            string ownerID = "123A";
            string location = "Edmonton";
            bool request = false;

            Asset asset = new Asset(
                assetId,
                name,
                description,
                addDate,
                price,
                priceDaily,
                priceWeekly,
                priceMonthly,
                ownerID,
                location,
                request
                );
            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IAssetRepository assetRepo = new AssetRepository(context);
                assetRepo.Add(asset);
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Asset.Count() == 1);
            }
        }

        [Fact]
        public void testDeleteAsset()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: "Delete_message_from_database")
    .Options;

            // Arrange
            var mockDB = new Mock<ApplicationDbContext>();
            var mockUser = new Mock<UserManager<ApplicationUser>>();
            var controller = new AssetsController(mockDB.Object, mockUser.Object);

            //Mock Asset
            int assetId = 1;
            string name = "Bob Cat";
            string description = "Fast and strong";
            DateTime addDate = DateTime.Now;
            decimal price = 5;
            decimal priceDaily = 5;
            decimal priceWeekly = 5;
            decimal priceMonthly = 5;
            string ownerID = "123A";
            string location = "Edmonton";
            bool request = false;

            Asset asset = new Asset(
                assetId,
                name,
                description,
                addDate,
                price,
                priceDaily,
                priceWeekly,
                priceMonthly,
                ownerID,
                location,
                request
                );
            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IAssetRepository assetRepo = new AssetRepository(context);
                assetRepo.Add(asset);
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Asset.Count() == 1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Asset.Count() == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IAssetRepository assetRepo = new AssetRepository(context);
                assetRepo.Remove(assetId);
            }

            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Message.Count() == 0);
            }
        }
    }
}
