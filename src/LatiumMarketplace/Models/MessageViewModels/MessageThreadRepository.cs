using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Data;
using Microsoft.EntityFrameworkCore;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public class MessageThreadRepository : IMessageThreadRepository
    {
        private ApplicationDbContext _context;

        public MessageThreadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddMessageThread(MessageThread messageThread)
        {
            _context.Add(messageThread);
            return;
        }

        public void DeleteMessageThread(Guid messageIDThread)
        {
            if (messageIDThread == null)
            {
                throw new ArgumentNullException("The messageID given was null. No messageID's are null.");
            }

            var messageThread = _context.MessageThread.Single(m => m.id == messageIDThread);
            if (messageThread == null)
            {
                throw new Exception("No message found by the given messageID");
            };
            _context.MessageThread.Remove(messageThread);
            return;
        }

        public IEnumerable<MessageThread> GetAllMessages(string userID)
        {
            if (userID == null)
            {
                throw new ArgumentNullException("The messageThreadID given was null, messageThreadID's are null.");
            }

            var messageThreads = _context.MessageThread.Include(m => m.asset).Where(m => m.SenderId == userID || m.RecieverId == userID);
            if (messageThreads == null)
            {
                throw new KeyNotFoundException("No matching messageThreadID found by the given messageThreadID");
            }
            return messageThreads;
        }

        public MessageThread GetMessageThreadByID(Guid messageThreadID)
        {
            if (messageThreadID == null)
            {
                throw new ArgumentNullException("The messageThreadID given was null. messageThreadID's are null.");
            }
            MessageThread messageThread = null;
            try
            {
                messageThread = _context.MessageThread.Single(m => m.id == messageThreadID);
            }
            catch
            {
                throw new KeyNotFoundException("No matching messageThreadID found by the given messageThreadID");
            }
            return messageThread;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}