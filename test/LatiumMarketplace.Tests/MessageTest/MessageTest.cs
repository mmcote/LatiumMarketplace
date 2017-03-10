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
    public class MessageTest
    {
        // This covers testing for adding/saving a message and retrieving the message
        [Fact]
        public void testAddMessage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_message_to_database")
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
                IMessageRepository messageRepo = new MessageRepository(context);
                var messageRecieved = messageRepo.GetMessageByID(message.id);
                Assert.True(messageRecieved.Subject == subject);
            }
        }

        // Test to see if we can delete a message
        [Fact]
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
        }
    }
}
