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

namespace LatiumMarketplace.Tests
{
    public class MessageTest
    {
        private ApplicationDbContext _ApplicationDbContext;

        public MessageTest()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase();

            _ApplicationDbContext = new ApplicationDbContext(builder.Options);
        }

        [Fact]
        public void testMockUsers()
        {
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

            IMessageRepository messageRepo = new MessageRepository(_ApplicationDbContext);
            messageRepo.AddMessage(message);
            messageRepo.Save();

            var messageRecieved = messageRepo.GetMessageByID(message.id);
            Assert.True(messageRecieved.SenderId == mockSenderID);
        }
    }
}
