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
        public string Name { get; set; }

        // Navigation properties for one-to-many relationship between
        // Asset and Accesories.
        // One asset can have multiple accessories entered by a particular user
        public int AssetId { get; set; }
        [ForeignKey("AssetId")]
        public Asset Asset { get; set; }
    }
}
