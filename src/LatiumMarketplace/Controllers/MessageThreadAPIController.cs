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

namespace LatiumMarketplace.Controllers
{
    [Produces("application/json")]
    [Route("api/MessageThreadAPI")]
    public class MessageThreadAPIController : Controller
    {
        private IMessageThreadRepository _messageThreadRepository;
        private IMessageRepository _messageRepository;
        public MessageThreadAPIController(ApplicationDbContext context)
        {
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
            Message message = new Message(input.Subject, input.Body);
            _messageRepository.AddMessage(message);
            MessageThread messageThread = new MessageThread(input.SenderId, input.RecieverId);
            messageThread.messages.Add(message);
            _messageThreadRepository.AddMessageThread(messageThread);
            _messageThreadRepository.Save();
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
