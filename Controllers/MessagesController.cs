using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectComp1640.Chat;
using ProjectComp1640.Model;
using ProjectComp1640.Dtos.Mess;

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

        [Authorize] // Yêu cầu đăng nhập
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            var senderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(senderId))
                return Unauthorized("Không thể xác định người gửi.");

            if (string.IsNullOrEmpty(messageDto.ReceiverId))
                return BadRequest("Người nhận không hợp lệ.");

            await _messageService.SendMessage(senderId, messageDto.ReceiverId, messageDto.Content);

            return Ok(new { message = "Tin nhắn đã gửi thành công!" });
        }
        [Authorize]
        [HttpGet("conversation/{receiverId}")]
        public async Task<IActionResult> GetConversation(string receiverId)
        {
            var senderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(senderId))
                return Unauthorized("Không thể xác định người gửi.");

            var messages = await _messageService.GetMessages(senderId, receiverId);

            return Ok(messages);
        }
    }

}
