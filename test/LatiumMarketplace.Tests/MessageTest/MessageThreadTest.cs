using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.MessageViewModels;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace LatiumMarketplace.Tests.MessageTest
{
    public class MessageThreadTest
    {
        // This covers testing for adding/saving a message and retrieving the message
        [Fact]
        public void testAddMessageThread()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_messageThread_to_database")
                .Options;

            var mockSender = new Mock<ApplicationUser>();
            var mockReciever = new Mock<ApplicationUser>();

            string mockSenderID = "ab3+10ds2";
            string mockRecieverID = "rg9_x1t\a";

            mockSender.Setup(sender => sender.Id).Returns(mockSenderID);
            mockReciever.Setup(reciever => reciever.Id).Returns(mockRecieverID);

            MessageThread messageThread = new MessageThread(mockSender.Object.Id, mockReciever.Object.Id);

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                messageThreadRepo.AddMessageThread(messageThread);
                Assert.True(context.MessageThread.Count() == 1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                var messageThreadRecieved = messageThreadRepo.GetMessageThreadByID(messageThread.id);
                Assert.True(messageThreadRecieved.SenderId == mockSender.Object.Id);
            }
        }

        [Fact]
        public void testGetMessagesByUserID()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_messageThread_by_userID_to_database")
                .Options;

            var mockSender = new Mock<ApplicationUser>();
            var mockReciever = new Mock<ApplicationUser>();

            string mockSenderID = "ab3+10ds2";
            string mockRecieverID = "rg9_x1t\a";

            mockSender.Setup(sender => sender.Id).Returns(mockSenderID);
            mockReciever.Setup(reciever => reciever.Id).Returns(mockRecieverID);

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                MessageThread messageThread = new MessageThread(mockSender.Object.Id, mockReciever.Object.Id);
                messageThreadRepo.AddMessageThread(messageThread);
                messageThread = new MessageThread(mockSender.Object.Id, mockReciever.Object.Id);
                messageThreadRepo.AddMessageThread(messageThread);
                messageThread = new MessageThread(mockSender.Object.Id, mockReciever.Object.Id);
                messageThreadRepo.AddMessageThread(messageThread);
                Assert.True(context.MessageThread.Count() == 3);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                var messageThreadsRecieved = messageThreadRepo.GetAllMessages(mockSender.Object.Id);
                Assert.True(messageThreadsRecieved.Count() == 3);
            }

            string mockDifferentSenderID = "eb1-15es2";
            string mockDifferentRecieverID = "va9_xxc\a";

            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                MessageThread messageThread = new MessageThread(mockDifferentSenderID, mockDifferentRecieverID);
                messageThreadRepo.AddMessageThread(messageThread);
                var messageThreadsRecieved = messageThreadRepo.GetAllMessages(mockSender.Object.Id);
                Assert.True(messageThreadsRecieved.Count() == 3);
            }
        }

        [Fact]
        public void testDeleteMessageThread()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_messageThread_to_database")
                .Options;

            var mockSender = new Mock<ApplicationUser>();
            var mockReciever = new Mock<ApplicationUser>();

            string mockSenderID = "ab3+10ds2";
            string mockRecieverID = "rg9_x1t\a";

            mockSender.Setup(sender => sender.Id).Returns(mockSenderID);
            mockReciever.Setup(reciever => reciever.Id).Returns(mockRecieverID);

            MessageThread messageThread = new MessageThread(mockSender.Object.Id, mockReciever.Object.Id);

            using (ApplicationDbContext context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                messageThreadRepo.AddMessageThread(messageThread);
                Assert.True(context.MessageThread.Count() == 1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                IMessageThreadRepository messageThreadRepo = new MessageThreadRepository(context);
                messageThreadRepo.DeleteMessageThread(messageThread.id);
                Assert.True(context.MessageThread.Count() == 0);
            }
        }
    }
}
