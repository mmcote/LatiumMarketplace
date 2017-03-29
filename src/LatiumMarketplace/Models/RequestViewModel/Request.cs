using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Models.BidViewModels;
using LatiumMarketplace.Models.MessageViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.RequestViewModel
{
    public class Request
    {
        public Request() { }

        public Request(int RequestID, string Name, string Description, string Duration, string OwnerID, string OwnerName, string Address, string Accessory)
        {
            requestID = RequestID;
            name = Name;
            description = Description;
            duration = Duration;
            ownerID = OwnerID;
            ownerName = OwnerName;
            address = Address;
            accessory = Accessory;
        }

        [Key]
        public int requestID { get; set; }
        [Display(Name = "Request Name")]
        public string name { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Length/Duration")]
        public string duration { get; set; }
        public string ownerID { get; set; }
        [Display(Name = "Owner")]
        public string ownerName { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "Accessory")]
        public string accessory { get; set; }
        public virtual List<MessageThread> MessageThreads { get; set; }
        public virtual List<Bid> Bids { get; set; }

        // Navigation properties for one-to-many relationship between Asset and City.
        // One asset has only one city
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        [Display(Name = "City")]
        public City City { get; set; }

        // Navigation properties for one-to-one relationship between Asset and AccessoryList.
        [ForeignKey("AccessoryListId")]
        public int? AccessoryListId { get; set; }
        public AccessoryList AccessoryList { get; set; }


    }
}
