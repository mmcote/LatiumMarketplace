using LatiumMarketplace.Models.AssetViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.BidViewModels
{
    public class Bid
    {

        private Bid()
        {
            //emtpy constructor
        }



        [Key]
        public int bidId { get; set; }

        [Required]
        [Display(Name = "Bid *")]
        public decimal bidPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date *")]
        public DateTime startDate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date *")]
        public DateTime endDate { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        
        //public bool bidType { get; set; }
        [Display(Name = "User")]
        public string bidder { get; set; }

        //[Display(Name = "Asset ID")]
        [ForeignKey("AssetId")]
        public Asset asset { get; set; }
    }
    


}
