﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Models.MessageViewModels
{
    /// <summary>
    /// The only time that a message thread will be created will be through an asset, and
    /// you can only start the thread if you send the initial message (no empty threads)
    /// </summary>
    public class MessageThreadDTO
    {
        public MessageThreadDTO() { }
        public MessageThreadDTO(string senderId, string recieverId, string subject, string body, int assetId=0)
        {
            AssetId = assetId;
            SenderId = senderId;
            RecieverId = recieverId;
            Subject = subject;
            Body = body;
        }

        [Required]
        public int AssetId { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string RecieverId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public bool IsSender { get; set; }
    }

    public class MessageThread
    {
        public MessageThread() { }

        public MessageThread(string senderId, string recieverId)
        {
            id = Guid.NewGuid();
            SenderId = senderId;
            RecieverId = recieverId;
            messages = new List<Message>();
            SenderUnreadMessageCount = 0;
            RecieverUnreadMessageCount = 0;
        }

        [Key]
        public Guid id { get; set; }

        [Required]
        [Display(Name = "Sender ID")]
        public string SenderId { get; set;}

        [Display(Name = "Sender Email")]
        public string SenderEmail { get; set; }

        [Required]
        [Display(Name = "Reciever ID")]
        public string RecieverId { get; set; }

        [Display(Name = "Reciever Email")]
        public string RecieverEmail { get; set; }

        public virtual List<Message> messages { get; set; }

        [Display(Name = "Asset ID")]
        [ForeignKey("Assetid")]
        public Asset asset { get; set; }

        [Required]
        [Display(Name = "Last Updated")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public int SenderUnreadMessageCount { get; set; }
        public int RecieverUnreadMessageCount { get; set; }

    }
}
