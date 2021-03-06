﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.MessageViewModels;
using LatiumMarketplace.Controllers;
using Moq;
using Xunit;


using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace LatiumMarketplace.Tests.MessageTest
{
    public class MessageAPITest
    {
        [Fact]
        public void testPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "API_Post_Database")
                            .Options;

            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                IMessageRepository messageRepo = new MessageRepository(context);

                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo,messageThreadRepo,mockConnectionManager.Object);
                OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 0);
            }

            MessageThread messageThread = new MessageThread();
            messageThread.id = Guid.NewGuid();

            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                IMessageRepository messageRepo = new MessageRepository(context);

                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                context.MessageThread.Add(messageThread);
                context.SaveChanges();

                string subject = "This is a test subject.";
                string body = "This is a test body";

                MessageDTO messageDTO = new MessageDTO(subject, body, messageThread.id.ToString());
                controller.Post(messageDTO);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                IMessageRepository messageRepo = new MessageRepository(context);

                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);

                OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 1);
            }
        }

        [Fact]
        public void testGetInd()
        {
            string subject = "This is a test subject.";
            string body = "This is a test body";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "API_GetInd_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 0);
            }

            MessageThread messageThread = new MessageThread();
            messageThread.id = Guid.NewGuid();

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                context.MessageThread.Add(messageThread);
                context.SaveChanges();
                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                MessageDTO messageDTO = new MessageDTO(subject, body, messageThread.id.ToString());
                controller.Post(messageDTO);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                Guid messageIdGuid = context.Message.Single(m => m.Subject == subject).id;
                OkObjectResult messageRetrievedObject = (OkObjectResult)controller.Get(messageIdGuid.ToString());
                Message messageRetrieved = (Message)messageRetrievedObject.Value;
                Assert.True(messageRetrieved.id == messageIdGuid);
            }
        }

        [Fact]
        public void testDelete()
        {
            string subject = "This is a test subject.";
            string body = "This is a test body";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "API_Delete_Database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);

                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object); OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 0);
            }

            MessageThread messageThread = new MessageThread();
            messageThread.id = Guid.NewGuid();

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                context.MessageThread.Add(messageThread);
                context.SaveChanges();
                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object); MessageDTO messageDTO = new MessageDTO(subject, body, messageThread.id.ToString());
                controller.Post(messageDTO);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);

                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                OkObjectResult getResult = (OkObjectResult)controller.Get();
                List<Message> messageList = (List<Message>)getResult.Value;
                Assert.True(messageList.Count == 1);
            }

            using (var context = new ApplicationDbContext(options))
            {
                IMessageRepository messageRepo = new MessageRepository(context);
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);

                var mockConnectionManager = new Mock<IConnectionManager>();

                var controller = new MessagesAPIController(messageRepo, messageThreadRepo, mockConnectionManager.Object);
                Guid messageIdGuid = context.Message.Single(m => m.Subject == subject).id;
                OkObjectResult messageDeleted = (OkObjectResult)controller.Delete(messageIdGuid.ToString());

                Assert.True((bool)messageDeleted.Value);
            }
        }
    }
}
