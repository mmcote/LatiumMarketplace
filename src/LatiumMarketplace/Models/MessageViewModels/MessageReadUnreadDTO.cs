using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    /// <summary>
    /// MessageReadUnreadDTO is used to mark a message from the view.
    /// </summary>
    public class MessageReadUnreadDTO
    {
        public string Id;
        public bool IsSender;
    }
}
