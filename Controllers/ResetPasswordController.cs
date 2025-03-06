using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Data;
using ProjectComp1640.Dtos.Account;
using ProjectComp1640.Model;
using System.Threading.Tasks;

namespace ProjectComp1640.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordController(ApplicationDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDtos resetPassword)
        {
            if (string.IsNullOrEmpty(resetPassword.Email) || string.IsNullOrEmpty(resetPassword.NewPassword))
            {
                return BadRequest("Email hoặc mật khẩu không hợp lệ.");
            }

            // Tìm user theo email
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }

            // Hash mật khẩu mới
            var passwordHasher = new PasswordHasher<AppUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, resetPassword.NewPassword);

            // Cập nhật mật khẩu
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Mật khẩu đã được cập nhật thành công.");
            }

            return BadRequest("Cập nhật mật khẩu thất bại.");
        }
    }


}
