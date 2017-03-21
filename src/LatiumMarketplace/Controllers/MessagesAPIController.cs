using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using LatiumMarketplace.Data;
using LatiumMarketplace.Models.MessageViewModels;

/*
 * MessagesAPIController can be used to get certain messages
 * without accessing them through the messageThread. Although
 * to post a message it should be noted that you must submit
 * a messageThread to append to.
 */
namespace LatiumMarketplace.Controllers
{
    [Produces("application/json")]
    [Route("api/MessagesAPI")]
    public class MessagesAPIController : Controller
    {
        private IMessageRepository _messageRepository;
        private IMessageThreadRepository _messageThreadRepository;

        public MessagesAPIController(IMessageRepository messageRepository, IMessageThreadRepository messageThreadRepository)
        {
            _messageThreadRepository = messageThreadRepository;
            _messageRepository = messageRepository;
        }

        // GET: api/MessagesAPI
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_messageRepository.GetAll());
        }

        // GET: api/MessagesAPI/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            Guid guid = Guid.Parse(id);
            try
            {
                Message message = _messageRepository.GetMessageByID(guid);
                return new OkObjectResult(message);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        // GET: api/MessagesAPI/5
        [HttpGet("{id}", Name = "GetAll")]
        public IActionResult GetAllRelatedToThread([FromBody]string id)
        {
            Guid guid = Guid.Parse(id);
            try
            {
                IEnumerable<Message> messages = _messageRepository.GetAllMessagesByThreadId(guid);
                return new OkObjectResult(messages.OrderBy(m => m.SendDate));
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        // POST: api/MessagesAPI
        [HttpPost]
        public IActionResult Post([FromBody]MessageDTO messageDTO)
        {
            Message message = new Message(messageDTO.subject, messageDTO.body);
            Guid guid = Guid.Parse(messageDTO.messageThreadId);
            try
            {
                message.messageThread = _messageThreadRepository.GetMessageThreadByID(guid);
                message.messageThread.LastUpdateDate = DateTime.Now;
            }
            catch (KeyNotFoundException)
            {
                return new BadRequestResult();
            }
            _messageRepository.AddMessage(message);
            _messageRepository.Save();
            return new OkResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Guid guid = Guid.Parse(id);
            try
            {
                Message message = _messageRepository.GetMessageByID(guid);
                if(message == null)
                {
                    return null;
                }
                _messageRepository.DeleteMessage(guid);
                _messageRepository.Save();
                return new OkObjectResult(true);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
