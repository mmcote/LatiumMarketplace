using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Category
    {
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