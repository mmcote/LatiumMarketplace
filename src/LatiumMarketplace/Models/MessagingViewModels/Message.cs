using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LatiumMarketplace.Models.MessagingViewModels
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageID { get; set; }

        [Required]
        public Guid MessageThreadID { get; set; }

        [Required]
        [Display(Name = "Message Content")]
        public string MessageContent { get; set; }

        [Required]
        [Display(Name = "Date Sent")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime DateSent { get; set; }

        [Required]
        public string SenderID { get; set; }

        [Required]
        public string RecipientID { get; set; }

        public bool Read { get; set; }

        public DateTime DateRead { get; set; }
    }
}
