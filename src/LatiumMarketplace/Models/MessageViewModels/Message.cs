using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    public class Message
    {
        public Guid id { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate { get; set; }
        public string Body { get; set; }
    }
}
