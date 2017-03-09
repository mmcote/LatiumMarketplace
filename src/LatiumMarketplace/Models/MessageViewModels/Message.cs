using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LatiumMarketplace.Models.MessageViewModels
{
    // This is a data transfer object to transfer the user
    // specified fields of a message (used for the API post method)
    public class MessageDTO
    {
        public MessageDTO(string subject, string body)
        {
            this.subject = subject;
            this.body = body;
        }
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class Message
    {
        /* Empty constructor needed for EF to materialize objects that are the results
         * of queries.
         * 
         * From microsoft:
         * While the default generated classes have an automatically supplied public 
         * parameterless constructor, there's nothing in the framework that requires 
         * that it be public. There must be a parameterless constructor, but it can be 
         * internal or private. You can try a simple example by taking the generated 
         * classes and adding in a private parameterless constructor. At that point the 
         * generated factory method will work to create instances for users, but the 
         * system will use the private parameterless constructor to materialize objects 
         * that are the results of queries. 
         */
        private Message() { }

        public Message(string subject, string body)
        {
            Subject = subject;
            Body = body;
            SendDate = DateTime.Now;
        }

        [Key]
        public Guid id { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Date Sent")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime SendDate { get; set; }

        // Not required as the subject may be simply enough to answer a message
        public string Body { get; set; }

        [ForeignKey("MessageThreadid")]
        public MessageThread messageThread { get; set; }
    }
}
