using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;

using LatiumMarketplace.Data;
using LatiumMarketplace.Models;
using LatiumMarketplace.Models.MessageViewModels;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using LatiumMarketplace.Hubs;

namespace LatiumMarketplace.Controllers
{
    [Produces("application/json")]
    [Route("api/MessageThreadAPI")]
    public class MessageThreadAPIController : ApiHubController<Broadcaster>
    {
        private IMessageThreadRepository _messageThreadRepository;
        private IMessageRepository _messageRepository;
        private ApplicationDbContext _context;

        public MessageThreadAPIController(ApplicationDbContext context, IConnectionManager connectionManager)
            : base(connectionManager)
        {
            _context = context;
            _messageRepository = new MessageRepository(context);
            _messageThreadRepository = new MessageThreadRepository(context);
        }

        // GET: api/MessageThreadAPI
        [HttpGet]
        public IActionResult Get([FromBody]string userId = "")
        {
            IEnumerable<MessageThread> userMessageTheads = null;
            try
            {
                userMessageTheads = _messageThreadRepository.GetAllMessages(userId);
            }
            catch (KeyNotFoundException)
            {
                userMessageTheads = new List<MessageThread>();
            }
            catch
            {
                return new BadRequestObjectResult("Invalid Request");
            }
            return new OkObjectResult(userMessageTheads.ToList());
        }

        // GET: api/MessageThreadAPI/5
        [HttpGet("{id}", Name = "GetMessageThread")]
        [Route("api/MessageThreadAPI/GetMessageThread")]
        public string GetMessageThread(string id)
        {
            Guid guid = Guid.Parse(id);
            var messageThread = _messageThreadRepository.GetMessageThreadByID(guid);
            return "value";
        }

        // POST: api/MessageThreadAPI
        [HttpPost]
        public void Post([FromBody]MessageThreadDTO input)
        {
            // The reciever will always be the seller
            Message message;
            try
            {
                var messageThreadRetrieved = _context.MessageThread.Single(m => m.asset.assetID == input.AssetId && m.SenderId == input.SenderId);
                message = new Message(input.Subject, input.Body);
                message.messageThread = messageThreadRetrieved;
                _messageRepository.AddMessage(message);
                _messageRepository.Save();
            }
            catch (InvalidOperationException)
            {
                message = new Message(input.Subject, input.Body);
                _messageRepository.AddMessage(message);
                MessageThread messageThread = new MessageThread(input.SenderId, input.RecieverId);
                messageThread.messages.Add(message);
                if (input.AssetId != 0)
                {
                    messageThread.asset = _context.Asset.Single(a => a.assetID == input.AssetId);
                }
                _messageThreadRepository.AddMessageThread(messageThread);
                _messageThreadRepository.Save();
            }


            var recieverUser = _context.User.Single(u => u.Id == input.RecieverId);

            // This notification redirect URL should put the user to the discussion
            string redirectURL = "/MessageThreads/Details/" + message.messageThread.id.ToString();
            Notification notification = new Notification(message.Subject, message.Body, redirectURL);
            Clients.Group(recieverUser.UserName).PresentNotification(notification);
        }

        // PUT: api/MessageThreadAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Guid guid = Guid.Parse(id);
            _messageThreadRepository.DeleteMessageThread(guid);
            _messageThreadRepository.Save();
        }
    }
}
