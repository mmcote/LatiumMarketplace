using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.MessageViewModels;
using LatiumMarketplace.Controllers;
using Moq;
using Xunit;

using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace LatiumMarketplace.Tests.AssetTest
{
    public class AssetsAPITest
    {
        [Fact]
        public void testGetAllAssets()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "API_GetInd01_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AssetsAPIController(context);
                Asset asset = new Asset();
                asset.name = "TestAsset01";
                context.Asset.Add(asset);
                context.SaveChanges();
                var assetList = context.Asset.ToList();
                Assert.True(assetList.Count == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AssetsAPIController(context);
                Asset asset = new Asset();
                asset.name = "TestAsset02";
                context.Asset.Add(asset);
                context.SaveChanges();
                var assetList = context.Asset.ToList();
                Assert.True(assetList.Count == 2);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AssetsAPIController(context);
                var result = (OkObjectResult) controller.GetAsset();
                List<Asset> assetList = (List<Asset>)result.Value;
                Assert.True(assetList.Count == 2);
            }
        }

        [Fact]
        public void testGetAssetId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "API_GetInd02_Database")
                .Options;

            int id = 0;
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AssetsAPIController(context);
                Asset asset = new Asset();
                asset.name = "TestAsset01";
                context.Asset.Add(asset);
                context.SaveChanges();

                var assetRetrieved = context.Asset.Single(a => a.name == asset.name);
                id = assetRetrieved.assetID;
                var assetList = context.Asset.ToList();
                Assert.True(assetList.Count == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AssetsAPIController(context);
                var result = (OkObjectResult)controller.GetAsset(id);
                Asset asset = (Asset)result.Value;
                Assert.True(asset.name == "TestAsset01");
            }
        }

        [Fact]
        public void testPostAsset()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "API_GetInd03_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var assetList = context.Asset.ToList();
                Assert.True(assetList.Count == 0);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AssetsAPIController(context);
                Asset asset = new Asset();
                asset.name = "TestAsset01";

                var result = (OkObjectResult)controller.PostAsset(asset);
                Asset returnAsset = (Asset)result.Value;
                Assert.True(returnAsset.name == "TestAsset01");
                Assert.True(context.Asset.ToList().Count == 1);
            }
        }
    }
}
