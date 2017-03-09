using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Asset
    {
        [Key]
        public int assetID { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        [Display(Name = "Avaliable Date")]
        [DataType(DataType.Date)]
        public DateTime addDate { get; set; }
        [Required]
        public decimal price { get; set; }
        //public List<bidList>
        public string ownerID { get; set; }
        [Required]
        public string location { get; set; }

        // Navigation properties for many-to-many relationship between
        // Asset and Category.
        // One asset can have many categories (one parent and one child).
        public ICollection<AssetCategory> AssetCategories { get; set; }

        // Navigation properties for one-to-one relationship between Asset and ImageGallery.
        // One image has only one gallery
        public int ImageGalleryId { get; set; }
        public ImageGallery ImageGallery { get; set; }
    }

}