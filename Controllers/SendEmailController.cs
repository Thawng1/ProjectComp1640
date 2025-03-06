using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using System.Threading.Tasks;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly ApplicationDBContext _context;
        public SendEmailController(EmailService emailService, ApplicationDBContext context)
        {
            _emailService = emailService;
            _context = context;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email không hợp lệ.");
            }
            // Kiểm tra email có tồn tại trong database không
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound("Email không tồn tại trong hệ thống.");
            }
            //Tạo nội dung email
            string subject = "Quên mật khẩu";
            string body = "<p>Bạn đã yêu cầu đặt lại mật khẩu. Nhấp vào link dưới để đặt lại:</p>" +
                          "<a href='https://yourapp.com/reset-password'>Đặt lại mật khẩu</a>";

            // Gửi email
            await _emailService.SendEmailAsync(email, subject, body);



            return Ok("Email đặt lại mật khẩu đã được gửi.");
        }
    }
}