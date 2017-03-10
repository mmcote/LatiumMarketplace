using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Models.MessageViewModels;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Asset
    {

        private Asset() { }

        public Asset(int AssetID, string Name, string Description, DateTime AddData, decimal Price, decimal PriceDaily, decimal PriceWeekly, decimal PriceMonthly, string OwnerID, string Location, bool Request)
        {
            assetID = AssetID;
            name = Name;
            description = Description;
            addDate = AddData;
            price = Price;
            priceDaily = PriceDaily;
            priceWeekly = PriceWeekly;
            priceMonthly = PriceMonthly;
            ownerID = OwnerID;
            location = Location;
            request = Request;
        }

        [Key]
        public int assetID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [Display(Name = "Avaliable Date")]
        [DataType(DataType.Date)]
        public DateTime addDate { get; set; }
        [Display(Name = "Sale Price")]
        public decimal price { get; set; }
        [Display(Name = "Daily Rate")]
        public decimal priceDaily { get; set; }
        [Display(Name = "Weekly Rate")]
        public decimal priceWeekly { get; set; }
        [Display(Name = "Monthly Price")]
        public decimal priceMonthly { get; set; }
        //public List<bidList>
        public string ownerID { get; set; }
        public string location { get; set; }
        public bool request { get; set; }
        public virtual List<MessageThread> MessageThreads { get; set; }
    }

}