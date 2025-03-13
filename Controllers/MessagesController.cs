using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectComp1640.Chat;
using ProjectComp1640.Model;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            await _messageService.SendMessage(messageDto.SenderId, messageDto.ReceiverId, messageDto.Content);
            return Ok(new { message = "Tin nhắn đã gửi thành công!" });
        }

        [HttpGet("conversation/{userId1}/{userId2}")]
        public async Task<ActionResult<List<Messages>>> GetConversation(string userId1, string userId2)
        {
            var messages = await _messageService.GetMessages(userId1, userId2);
            return Ok(messages);
        }
    }
    public class MessageDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
