using LatiumMarketplace.Models.AssetViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public class MessageDetailsView
    {
        public MessageDetailsView(string messageThreadId, IEnumerable<Message> messages, string opposingEmail, Asset asset = null)
        {
            MessageThreadId = messageThreadId;
            Messages = messages;
            DiscussedAsset = asset;
            OpposingEmail = opposingEmail;
        }
        public string MessageThreadId;
        public Asset DiscussedAsset;
        public string OpposingEmail;
        public IEnumerable<Message> Messages;
    }
}
