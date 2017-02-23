using LatiumMarketplace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessagingViewModels
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationDbContext _databaseContext;

        public MessageRepository(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void AddMessage(Message message)
        {
            _databaseContext.Add(message);
        }

        public void DeleteMessage(Guid messageID)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageByID(Guid messageID)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
