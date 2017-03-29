using LatiumMarketplace.Models.AssetViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public class MessageDetailsView
    {
        public MessageDetailsView(string messageThreadId, IEnumerable<Message> messages, string opposingEmail, bool isSender, Asset asset = null)
        {
            MessageThreadId = messageThreadId;
            Messages = messages;
            DiscussedAsset = asset;
            OpposingEmail = opposingEmail;
            IsSender = isSender;
        }
        public string MessageThreadId;
        public Asset DiscussedAsset;
        public string OpposingEmail;
        public IEnumerable<Message> Messages;
        public bool IsSender;
    }
}
