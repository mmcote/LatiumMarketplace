using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Category
    {
        /// <summary>
        /// This holds asset category and sub-category of given asset.
        /// </summary>
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }


        // Navigation properties for self-referecing Category Table
        // A parent category can have many other categories as sub-categories,
        // and one sub-caterogy only has one parent category
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategory { get; set; }

        // Navigation properties for many-to-many relationship between
        // Asset and Category.
        // One category can have many assets.
        public ICollection<AssetCategory> AssetCategories { get; set; }
    }
}
