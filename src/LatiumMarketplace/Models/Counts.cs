using LatiumMarketplace.Models.MessageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models
{
    public class Counts
    {
        public Counts()
        {
            UnreadMessages = 0;
            UnseenBids = 0;
        }
        public int UnreadMessages { get; set; }
        public int UnseenBids { get; set; }
    }
}
