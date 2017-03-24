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
        public async void testGetInd()
        {
            string subject = "This is a test subject.";
            string body = "This is a test body";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "API_GetInd_Database")
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

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                context.MessageThread.Add(messageThread);
                context.SaveChanges();
                var controller = new MessagesAPIController(messageRepo, messageThreadRepo);

                MessageDTO messageDTO = new MessageDTO(subject, body, messageThread.id.ToString());
                controller.Post(messageDTO);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo);

                OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo);

                Guid messageIdGuid = context.Message.Single(m => m.Subject == subject).id;
                OkObjectResult messageRetrievedObject = (OkObjectResult)controller.Get(messageIdGuid.ToString());
                Message messageRetrieved = (Message)messageRetrievedObject.Value;
                Assert.True(messageRetrieved.id == messageIdGuid);
            }
        }
    }
}
