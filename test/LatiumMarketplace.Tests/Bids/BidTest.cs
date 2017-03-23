using LatiumMarketplace.Data;
using LatiumMarketplace.Models.BidViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LatiumMarketplace.Tests.Bids
{
    public class BidTest
    {
        
        // This covers testing for adding/saving a bid
        [Fact]
        public void testAddBid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_bid_to_db")
                .Options;

            int price = 230;
            string description = "This is how much you owe me";
            var start = new DateTime(2017 / 01 / 12);
            var end = new DateTime(2017 / 01 / 12);
            Bid bid = new Bid();
            bid.bidPrice = price;
            bid.description = description;
            bid.startDate = start;
            bid.endDate = end;
           

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IBidRepository bidRepo = new BidRepository(context);
                bidRepo.AddBid(bid);
                bidRepo.Save();
                
            }
             
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                IBidRepository bidRepo = new BidRepository(context);
                var bidRecieved = bidRepo.GetBidByID(bid.bidId);
                Assert.False(bidRecieved.bidPrice == price);
            }
        }

        // Test to see if we can delete a message
        /*[Fact]
        public void testDeleteMessage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_message_from_database")
                .Options;

            string subject = "Test Subject";
            string body = "Test Body";

            Message message = new Message(
                subject,
                body
            );

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                messageRepo.AddMessage(message);
                messageRepo.Save();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Message.Count() == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                messageRepo.DeleteMessage(message.id);
                messageRepo.Save();
            }

            using (var context = new ApplicationDbContext(options))
            {
                Assert.True(context.Message.Count() == 0);
            }
        }*/
    }    
}
