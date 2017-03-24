using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Feature
    {
        /// <summary>
        /// This holds the features of given asset.
        /// </summary>
        public int FeatureId { get; set; }
        public int Year { get; set; }
        public string Cab { get; set; }
        public int Seats { get; set; }
        public int Odometer { get; set; }
        public decimal EngineHours { get; set; }
        public int NumberOfAxels { get; set; }
        public decimal FluelTankCapicity { get; set; }
        public bool IsForWheelDrive { get; set; }

        // Navigation properties for many-to-many relationship between
        // Asset and Feature.
        // One feature can be linked to many assets.
        public ICollection<AssetFeature> AssetFeatures { get; set; }

    }
}
