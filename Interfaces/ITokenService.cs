using ProjectComp1640.Model;
using System.Threading.Tasks;

namespace ProjectComp1640.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user); // 👈 Đảm bảo trả về Task<string>
    }
}
