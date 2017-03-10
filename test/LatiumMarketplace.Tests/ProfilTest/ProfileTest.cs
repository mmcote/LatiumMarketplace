using Xunit;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models.Repository;
using LatiumMarketplace.Models.IRepository;
using LatiumMarketplace.Models.ManageViewModels;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Models;

namespace LatiumMarketplace.Tests.ProfilTest
{
    public class ProfileTest
    {
        [Fact]
        public void testEdit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_user_to_db")
                .Options;

            var user = new ApplicationUser()
            {
                firstName = "Sarah",
                lastName = "Jones",
                description = "First"
            };

            var profile = user;
         
            profile.firstName = "Vic";
            profile.lastName = "Lee";
            profile.description = "Updated";

            Assert.False(profile.firstName == "Sarah");
            Assert.True(profile.description == "Updated");
        }
    }
}
  