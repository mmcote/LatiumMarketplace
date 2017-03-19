using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    /// <summary>
    /// This creates the many-to-many relationship between asset and category.
    /// An asset has a main category and a sub-category; and many asset could be
    /// contained inside those categories. Hence the many-to-many relationship.
    /// </summary>
    /// <remarks>
    /// http://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
    /// 
    /// In EF Core 1.1.0, it is necessary to include an entity in the model to 
    /// represent the join table, and then add navigation properties either
    /// side of the many-to-many relations that point to the join entity instead.
    /// </remarks>

    public class AssetCategory
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
