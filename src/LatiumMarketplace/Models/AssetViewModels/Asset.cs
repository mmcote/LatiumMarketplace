using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LatiumMarketplace.Models.AssetViewModels
{
    public class Asset
    {
        public int assetID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime addDate { get; set; }
        public decimal price { get; set; }
        //public List<bidList>
        public int ownerID { get; set; }
        public string location { get; set; }
    }
}