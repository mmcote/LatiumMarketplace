using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.MessageViewModels;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace LatiumMarketplace.Tests.MessageTest
{
    public class MessageTest
    {
        private DbContextOptionsBuilder<ApplicationDbContext> _builder;

        public MessageTest()
        {
            _builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase();
        }

        // This covers testing for adding/saving a message and retrieving the message
        [Fact]
        public void testAddMessage()
        {
            ApplicationDbContext context = new ApplicationDbContext(_builder.Options);
            var mockSender = new Mock<ApplicationUser>();
            var mockReciever = new Mock<ApplicationUser>();

            string mockSenderID = "ab3+10ds2";
            string mockRecieverID = "rg9_x1t\a";

            mockSender.Setup(sender => sender.Id).Returns(mockSenderID);
            mockReciever.Setup(reciever => reciever.Id).Returns(mockRecieverID);

            string subject = "Test Subject";
            string body = "Test Body";

            Message message = new Message(
                mockSender.Object.Id,
                mockReciever.Object.Id,
                subject,
                body
            );

            IMessageRepository messageRepo = new MessageRepository(context);
            messageRepo.AddMessage(message);
            messageRepo.Save();
            var messageRecieved = messageRepo.GetMessageByID(message.id);
            Assert.True(messageRecieved.SenderId == mockSenderID);
        }

        // Test to see if we can delete a message
        [Fact]
        public void testDeleteMessage()
        {
            ApplicationDbContext context = new ApplicationDbContext(_builder.Options);
            var mockSender = new Mock<ApplicationUser>();
            var mockReciever = new Mock<ApplicationUser>();

            string mockSenderID = "ab3+10ds2";
            string mockRecieverID = "rg9_x1t\a";

            mockSender.Setup(sender => sender.Id).Returns(mockSenderID);
            mockReciever.Setup(reciever => reciever.Id).Returns(mockRecieverID);

            string subject = "Test Subject";
            string body = "Test Body";

            Message message = new Message(
                mockSender.Object.Id,
                mockReciever.Object.Id,
                subject,
                body
            );
            IMessageRepository messageRepo = new MessageRepository(context);
            messageRepo.AddMessage(message);
            var count = context.Message.Count();
            Assert.True(count == 0);
            messageRepo.Save();
            count = context.Message.Count();
            Assert.True(count == 1); 
            messageRepo.DeleteMessage(message.id);
            messageRepo.Save();
            count = context.Message.Count();
            Assert.True(count == 0);
        }
    }
}
