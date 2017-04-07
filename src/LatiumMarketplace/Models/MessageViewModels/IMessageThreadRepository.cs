using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    /// <summary>
    /// IMessageThreadRespository has all the function signatures of the 
    /// MessageThreadRepository to access the database this repository pattern
    /// is used to allow flexibility with databases
    /// </summary>
    public interface IMessageThreadRepository
    {
        MessageThread GetMessageThreadByID(Guid messageThreadID);
        void AddMessageThread(MessageThread messageThread);
        void DeleteMessageThread(Guid messageIDThread);
        IEnumerable<MessageThread> GetAllMessages(string userID);
        void Save();
    }
}
