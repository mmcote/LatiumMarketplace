using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Data;
using Microsoft.EntityFrameworkCore;

namespace LatiumMarketplace.Models.MessageViewModels
{
    /// <summary>
    /// Message repository is the database implementation with entity framework
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddMessage(Message message)
        {
            _context.Add(message);
            return;
        }

        public void DeleteMessage(Guid messageID)
        {
            if (messageID == null)
            {
                throw new ArgumentNullException("The messageID given was null. No messageID's are null.");
            }

            var message = _context.Message.Single(m => m.id == messageID);
            if (message == null)
            {
                throw new Exception("No message found by the given messageID");
            };
            _context.Message.Remove(message);
            return;
        }

        public Message GetMessageByID(Guid messageID)
        {
            if (messageID == null)
            {
                throw new ArgumentNullException("The messageID given was null. No messageID's are null.");
            }

            var message = _context.Message.Include(m => m.messageThread).Single(m => m.id == messageID);
            if (message == null)
            {
                throw new KeyNotFoundException("No message found by the given messageID");
            }

            return message;
        }

        public IEnumerable<Message> GetAll()
        {
            return _context.Message.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAllMessagesByThreadId(Guid threadID)
        {
            if (threadID == null)
            {
                throw new ArgumentNullException("The threadId given was null, threadId's are null.");
            }

            var messages = _context.Message.Where(m => m.messageThread.id == threadID);
            if (messages == null)
            {
                throw new KeyNotFoundException("No matching threadId found by the given threadId");
            }
            return messages;
        }

        public void MessageRead(Guid messageID, bool isSender)
        {
            if (messageID == null)
            {
                throw new ArgumentNullException("The messageID given was null. No messageID's are null.");
            }

            var message = _context.Message.Single(m => m.id == messageID);
            if (message == null)
            {
                throw new Exception("No message found by the given messageID");
            };
            if (isSender)
            {
                message.SenderUnread = false;
            }
            else
            {
                message.RecieverUnread = false;
            }
            return;
        }

        public void MessageUnread(Guid messageID, bool isSender)
        {
            if (messageID == null)
            {
                throw new ArgumentNullException("The messageID given was null. No messageID's are null.");
            }

            var message = _context.Message.Single(m => m.id == messageID);
            if (message == null)
            {
                throw new Exception("No message found by the given messageID");
            };
            if (isSender)
            {
                message.SenderUnread = true;
            }
            else
            {
                message.RecieverUnread = true;
            }
            return;
        }
    }
}
