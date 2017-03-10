using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public class MessageDetailsView
    {
        public MessageDetailsView(string messageThreadId, IEnumerable<Message> messages, string assetId = null)
        {
            MessageThreadId = messageThreadId;
            Messages = messages;
            AssetId = assetId;
        }
        public string MessageThreadId;
        public string AssetId;
        public IEnumerable<Message> Messages;
    }
}
