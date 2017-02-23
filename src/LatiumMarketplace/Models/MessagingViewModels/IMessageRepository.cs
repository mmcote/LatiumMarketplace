using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessagingViewModels
{
    public interface IMessageRepository
    {
        // Basic CRUD operations
        Message GetMessageByID(Guid messageID);
        void AddMessage(Message message);
        void DeleteMessage(Guid messageID);
        void UpdateMessage(Message message);
        void Save();
    }
}
