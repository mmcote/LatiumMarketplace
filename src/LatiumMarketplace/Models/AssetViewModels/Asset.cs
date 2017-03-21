using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Models.MessageViewModels;
using LatiumMarketplace.Models.BidViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Asset
    {

        public Asset() { }

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
        [Display(Name = "Asset ID")]
        public int assetID { get; set; }
        [Display(Name = "Asset Name")]
        public string name { get; set; }
        [Display(Name = "Description")]
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
        [Display(Name = "Owner ID")]
        public string ownerID { get; set; }
        [Display(Name = "Location")]
        public string location { get; set; }
        [Display(Name = "Accessory")]
        public string accessory { get; set; }
        public bool request { get; set; }
        public virtual List<MessageThread> MessageThreads { get; set; }
        public virtual List<Bid> Bids { get; set; }
        
        // Navigation properties for one-to-many relationship between
        // Asset and Make.
        // One asset can only have one make, but many makes can be shared by
        // multiple assets.
        public int MakeId { get; set; }
        [ForeignKey("MakeId")]
        public Make Make { get; set; }

        // Navigation properties for many-to-many relationship between
        // Asset and Category.
        // One asset can have many categories (one parent and one child).
        public ICollection<AssetCategory> AssetCategories { get; set; }

        // Navigation properties for one-to-one relationship between Asset and ImageGallery.
        // One image has only one gallery
        [ForeignKey("ImageGalleryId")]
        public int? ImageGalleryId { get; set; }
        public ImageGallery ImageGallery { get; set; }

        // Navigation properties for one-to-many relationship between
        // Asset and Accesories.
        // One asset can have multiple accessories entered by a particular user
        public ICollection<Accessory> Accessory { get; set; }
    }

}