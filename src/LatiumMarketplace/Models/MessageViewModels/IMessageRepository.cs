using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    /// <summary>
    /// IMessageRespository has all the function signatures of the 
    /// MessageRepository to access the database this repository pattern
    /// is used to allow flexibility with databases
    /// </summary>
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
