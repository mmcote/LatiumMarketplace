using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LatiumMarketplace.Models.AssetViewModels;

namespace LatiumMarketplace.Models.MessageViewModels
{
    // The only time that a message thread will be created will be through an asset, and
    // you can only start the thread if you send the initial message (no empty threads)
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
        }

        [Key]
        public Guid id { get; set; }

        [Required]
        public string SenderId { get; set;}

        [Required]
        public string RecieverId { get; set; }

        public virtual List<Message> messages { get; set; }

        [ForeignKey("Assetid")]
        public Asset asset { get; set; }
    }
}
