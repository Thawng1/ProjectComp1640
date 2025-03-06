using Microsoft.AspNetCore.Identity;
using ProjectComp1640.Data;
using ProjectComp1640.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

public class DataSeeder
{
    public static async Task SeedAdminUser(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext context)
    {
        // 1️⃣ Kiểm tra Role "Admin" có tồn tại không
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            Console.WriteLine("✅ Role 'Admin' đã được tạo.");
        }
        else
        {
            Console.WriteLine("⚠️ Role 'Admin' đã tồn tại.");
        }

        // 2️⃣ Kiểm tra tài khoản admin có tồn tại không
        var adminUser = await userManager.FindByNameAsync("admin");

        if (adminUser == null)
        {
            Console.WriteLine("⚠️ Tài khoản Admin chưa tồn tại, tạo mới...");

            var newAdmin = new AppUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@123456");

            if (result.Succeeded)
            {
                Console.WriteLine("✅ Tài khoản Admin được tạo thành công.");

                // 3️⃣ Gán role Admin
                var roleResult = await userManager.AddToRoleAsync(newAdmin, "Admin");

                if (roleResult.Succeeded)
                {
                    Console.WriteLine("✅ Tài khoản Admin đã được gán role 'Admin'.");
                }
                else
                {
                    Console.WriteLine("❌ Lỗi khi gán role Admin:");
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($"   - {error.Description}");
                    }
                }

                // 4️⃣ Thêm thông tin Admin vào bảng Admins
                var adminData = new Admin
                {
                    UserId = newAdmin.Id,
                    Department = "IT Department"
                };

                context.Admins.Add(adminData);
                await context.SaveChangesAsync();
                Console.WriteLine("✅ Thông tin Admin đã được lưu vào database.");
            }
            else
            {
                Console.WriteLine("❌ Lỗi khi tạo tài khoản Admin:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"   - {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("✅ Tài khoản Admin đã tồn tại.");

            // Kiểm tra role có bị thiếu không
            var userRoles = await userManager.GetRolesAsync(adminUser);
            if (!userRoles.Contains("Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine("✅ Role 'Admin' đã được gán lại.");
            }
        }
    }
}
