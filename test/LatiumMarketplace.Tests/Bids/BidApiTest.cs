//using LatiumMarketplace.Controllers;
//using LatiumMarketplace.Data;
//using LatiumMarketplace.Models.BidViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//namespace LatiumMarketplace.Tests.Bids
//{
//    public class BidApiTest
//    {
//        [Fact]
//        public void testPostBid()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                            .UseInMemoryDatabase(databaseName: "Post_bid_db")
//                            .Options;

//            using (var context = new ApplicationDbContext(options))
//            {
//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);
//                OkObjectResult getResult = (OkObjectResult)controller.GetAll();
//                List<Bid> bidList = (List<Bid>)getResult.Value;
//                Assert.True(bidList.Count == 0);
//            }

//            using (var context = new ApplicationDbContext(options))
//            {
//                int price = 230;
//                string description = "This is how much you owe me";
//                var start = new DateTime(2017 / 01 / 12);
//                var end = new DateTime(2017 / 01 / 12);
//                Bid bid = new Bid();
//                bid.bidPrice = price;
//                bid.description = description;
//                bid.startDate = start;
//                bid.endDate = end;

//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);
//                controller.Post(bid);
//            }

//            using (var context = new ApplicationDbContext(options))
//            {
//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);

//                OkObjectResult getResult = (OkObjectResult)controller.GetAll();
//                List<Bid> bidList = (List<Bid>)getResult.Value;
//                Assert.True(bidList.Count == 1);
//            }
//        }

//        [Fact]
//        public void testGetBid()
//        {
//            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//                            .UseInMemoryDatabase(databaseName: "Get_bid_db")
//                            .Options;

//            using (var context = new ApplicationDbContext(options))
//            {
//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);
//                OkObjectResult getResult = (OkObjectResult)controller.GetAll();
//                List<Bid> bidList = (List<Bid>)getResult.Value;
//                Assert.True(bidList.Count == 0);
//            }

//            using (var context = new ApplicationDbContext(options))
//            {
//                int price = 230;
//                string description = "This is how much you owe me";
//                var start = new DateTime(2017 / 01 / 12);
//                var end = new DateTime(2017 / 01 / 12);
//                Bid bid = new Bid();
//                bid.bidPrice = price;
//                bid.description = description;
//                bid.startDate = start;
//                bid.endDate = end;

//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);
//                controller.Post(bid);
//            }

//            using (var context = new ApplicationDbContext(options))
//            {
//                int price = 250;
//                string description = "This is how much you owe me";
//                var start = new DateTime(2017 / 01 / 12);
//                var end = new DateTime(2017 / 01 / 12);
//                Bid bid = new Bid();
//                bid.bidPrice = price;
//                bid.description = description;
//                bid.startDate = start;
//                bid.endDate = end;

//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);
//                controller.Post(bid);
//            }

//            using (var context = new ApplicationDbContext(options))
//            {
//                int price = 240;
//                string description = "This is how much you owe me";
//                var start = new DateTime(2017 / 01 / 12);
//                var end = new DateTime(2017 / 01 / 12);
//                Bid bid = new Bid();
//                bid.bidPrice = price;
//                bid.description = description;
//                bid.startDate = start;
//                bid.endDate = end;

//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);
//                controller.Post(bid);
//            }

//            using (var context = new ApplicationDbContext(options))
//            {
//                IBidRepository bidRepo = new BidRepository(context);
//                var controller = new BidsAPIController(context, bidRepo);

//                OkObjectResult getResult = (OkObjectResult)controller.GetAll();
//                List<Bid> bidList = (List<Bid>)getResult.Value;
//                Assert.True(bidList.Count == 3);
//            }
//        }

//    }
//}
