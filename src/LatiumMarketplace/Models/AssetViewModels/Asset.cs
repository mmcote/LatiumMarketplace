using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Asset
    {
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

    }

}