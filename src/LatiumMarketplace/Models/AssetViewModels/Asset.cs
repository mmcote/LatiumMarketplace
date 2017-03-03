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
        [Display(Name = "Avaliable Date")]
        [DataType(DataType.Date)]
        public DateTime addDate { get; set; }
        public decimal price { get; set; }
        //public List<bidList>
        public string ownerID { get; set; }
        public string location { get; set; }

    }

}