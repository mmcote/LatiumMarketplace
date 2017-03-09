using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class ImageGallery
    {
        [Key]
        public int ImageGalleryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Navigation properties for one-to-many relationship between Image and ImageGallery.
        // One gallery can have many images
        public ICollection<Image> Images { get; set; }

        // Navigation properties for one-to-one relationship between Asset and ImageGallery.
        // One gallery only has one asset
        public Asset Asset { get; set; }
    }
}
