using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public class MessageSendView
    {
        public MessageSendView(Asset asset, string subject, string body)
        {
            this.Asset = asset;
            this.Subject = subject;
            this.Body = body;
        }

        // This is what the thread needs to save
        public Asset Asset;

        // This is what the individual message will need to save
        public string Subject;
        public string Body;
    }
}
