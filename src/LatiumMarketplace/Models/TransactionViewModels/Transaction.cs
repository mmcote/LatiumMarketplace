using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LatiumMarketplace.Models.AssetViewModels;
using LatiumMarketplace.Models.BidViewModels;

namespace LatiumMarketplace.Models.TransactionViewModels
{
    public class Transaction
    {

        private Transaction()
        {
            // empty constructor
        }
        [Key]
        public Guid transactionId { get; set; }

        [Display(Name = "Total Amount")]
        public int price { get; set; }

        [Display(Name = "Time of Transaction")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime transactionDate { get; set; }

        
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        public DateTime start { get; set; }

        [Display(Name = "Return Date")]
        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        public DateTime end { get; set; }

        Asset asset { get; set; }

        Bid bid { get; set; }

        //[ForeignKey("ReviewID")]
        //Review review { get; set; }
    }
}
