using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public int? ImageGalleryId { get; set; }
        public string Title { get; set; }
        [Required]
        public string FileLink { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool isMain { get; set; }

        // Navigation properties for one-to-many relationship between Image and ImageGallery.
        // One image has only one gallery
        public ImageGallery ImageGallery { get; set; }
    }
}
