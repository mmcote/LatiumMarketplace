using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public interface IMessageThreadRepository
    {
        MessageThread GetMessageThreadByID(Guid messageThreadID);
        void AddMessageThread(MessageThread messageThread);
        void DeleteMessageThread(Guid messageIDThread);
        IEnumerable<MessageThread> GetAllMessages(string userID);
        void Save();
    }
}
