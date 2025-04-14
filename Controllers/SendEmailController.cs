using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Model;
using System.Net;
using System.Threading.Tasks;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SendEmailController(EmailService emailService, ApplicationDBContext context, UserManager<AppUser> userManager)
        {
            _emailService = emailService;
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email không hợp lệ.");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Email không tồn tại trong hệ thống.");

            // Tạo token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);

            // Tạo link chứa token + email
            var resetLink = $"https://victorious-smoke-0d0ea8a00.6.azurestaticapps.net/reset-password?email={email}&token={encodedToken}";


            string subject = "Quên mật khẩu";
            string body = $"<p>Bạn đã yêu cầu đặt lại mật khẩu.</p>" +
                          $"<p>Nhấp vào liên kết dưới đây để đặt lại mật khẩu:</p>" +
                          $"<a href='{resetLink}'>Đặt lại mật khẩu</a>";

            await _emailService.SendEmailAsync(email, subject, body);

            return Ok("Email đặt lại mật khẩu đã được gửi.");
        }

    }
}