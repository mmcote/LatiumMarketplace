using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Data;

namespace LatiumMarketplace.Models.MessageViewModels
{
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

            var message = _context.Message.Single(m => m.id == messageID);
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
    }
}
