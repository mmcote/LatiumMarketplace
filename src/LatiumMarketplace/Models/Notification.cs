using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models
{
    public class Notification
    {
        public Notification(string header, string body, string redirectUrl)
        {
            Header = header;
            Body = body;
            RedirectUrl = redirectUrl;
        }

        // The type of the notification can only be one of a few things
        //  - (0) A bid
        //  - (1) A choosen bid
        //  - (2) A Message
        public int type;
        public string Header;
        public string Body;
        public string RedirectUrl;

    }
}
