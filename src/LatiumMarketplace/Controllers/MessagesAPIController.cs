using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using LatiumMarketplace.Data;
using LatiumMarketplace.Models.MessageViewModels;

namespace LatiumMarketplace.Controllers
{
    [Produces("application/json")]
    [Route("api/MessagesAPI")]
    public class MessagesAPIController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesAPIController(IMessageRepository messageRepository)
        {
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

        // POST: api/MessagesAPI
        [HttpPost]
        public void Post([FromBody]MessageDTO messageDTO)
        {
            Message message = new Message(null, null, messageDTO.subject, messageDTO.body);
            _messageRepository.AddMessage(message);
            _messageRepository.Save();
        }
        
        // PUT: api/MessagesAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
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
