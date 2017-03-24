using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    /// <summary>
    /// This creates the many-to-many relationship between asset and feature.
    /// </summary>
    /// <remarks>
    /// http://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
    /// 
    /// In EF Core 1.1.0, it is necessary to include an entity in the model to 
    /// represent the join table, and then add navigation properties either
    /// side of the many-to-many relations that point to the join entity instead.
    /// </remarks>
    public class AssetFeature
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
