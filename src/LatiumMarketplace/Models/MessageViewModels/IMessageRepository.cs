using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public interface IMessageRepository
    {
        // Basic CRUD operations
        Message GetMessageByID(Guid messageID);
        IEnumerable<Message> GetAllMessagesByThreadId(Guid messageID);
        IEnumerable<Message> GetAll();
        void AddMessage(Message message);
        void DeleteMessage(Guid messageID);
        void MessageRead(Guid messageID, bool isSender);
        void MessageUnread(Guid messageID, bool isSender);
        void UpdateMessage(Message message);
        void Save();
    }
}
