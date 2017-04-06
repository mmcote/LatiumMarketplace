using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Accessory
    {
        [Key]
        public int AccessoryId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        // Navigation properties for one-to-many relationship between Accessory and AccessoryList.
        // One accessory has only one AccessoryList
        public int? AccessoryListId { get; set; }
        [ForeignKey("AccessoryListId")]
        public AccessoryList AccessoryList { get; set; }
    }
}
