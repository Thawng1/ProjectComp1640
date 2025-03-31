using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectComp1640.Interfaces;
using ProjectComp1640.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectComp1640.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }

        public async Task<string> CreateToken(AppUser user) // 👈 Đảm bảo kiểu trả về là Task<string>
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            // 🔥 Lấy danh sách role của user và thêm vào token
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // 👈 Đảm bảo role được thêm vào token
            }
            // 🔎 Nếu user có role "Student", lấy StudentId
            if (roles.Contains("Student"))
            {
                var student = await _userManager.Users
                    .Include(u => u.Students) // Thêm Include để load dữ liệu
                    .Where(u => u.Id == user.Id)
                    .Select(u => u.Students) // Lấy student đầu tiên nếu có
                    .FirstOrDefaultAsync();

                if (student != null)
                {
                    claims.Add(new Claim("StudentId", student.Id.ToString()));
                }
            }

            // 🔎 Nếu user có role "Tutor", lấy TutorId
            if (roles.Contains("Tutor"))
            {
                var tutor = await _userManager.Users
                    .Include(u => u.Tutors) // Thêm Include để load dữ liệu
                    .Where(u => u.Id == user.Id)
                    .Select(u => u.Tutors)
                    .FirstOrDefaultAsync();

                if (tutor != null)
                {
                    claims.Add(new Claim("TutorId", tutor.Id.ToString()));
                }
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // Token có hiệu lực trong 7 ngày
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
