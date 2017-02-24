using LatiumMarketplace.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Data;

namespace LatiumMarketplace.Tests.AssetTest
{
    public class GettingAssetName_Should
    {
        // Running test with some simple facts
        [Fact]
        public void ReturnTrue_WhenTheyAreEqualLocally()
        {
            // Arrange
            var asset = new Asset()
            {
                name = "Truck",
                description = "Good running condition"
            };

            // Act


            // Assert
            Assert.Equal("Truck", asset.name);
        }

        [Fact]
        public async void ReturnTrue_WhenTheyAreEqual()
        {
            /** TO_DO: This doesn't work. Need to find a way to get
             *  the asset from the DB.
             */ 
            


            // Arrange


            // Act
            ApplicationDbContext context = null;
            var controller = new Controllers.AssetsController(context);

            Asset asset = (Asset)await controller.Details(2);


            // Assert
            Assert.Equal("awd", asset.name);
        }
    }
}
