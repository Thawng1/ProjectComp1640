Dưới đây là một bản **README.md** đầy đủ và chi tiết cho dự án **ProjectComp1640**, dựa trên mã nguồn và các chức năng đã phân tích. Bạn có thể sao chép nội dung này vào tệp `README.md` trong thư mục gốc của dự án.

-----

# ProjectComp1640 - Hệ Thống Quản Lý Trung Tâm Đào Tạo

**ProjectComp1640** là một hệ thống Backend Web API mạnh mẽ được xây dựng trên nền tảng **ASP.NET Core (.NET 8)**. Dự án cung cấp giải pháp toàn diện để quản lý các hoạt động của một trung tâm giáo dục hoặc gia sư, bao gồm quản lý người dùng, lớp học, thời khóa biểu, blog và tích hợp các tính năng giao tiếp thời gian thực (Real-time).

## 🚀 Tính Năng Chính

### 🔐 1. Xác thực & Phân quyền (Authentication & Authorization)

  - **Đăng ký & Đăng nhập:** Hỗ trợ đăng nhập an toàn sử dụng **JWT (JSON Web Token)**.
  - **Quản lý Vai trò (RBAC):** Hệ thống phân quyền rõ ràng cho 3 đối tượng:
      - **Admin:** Quản trị viên hệ thống.
      - **Tutor:** Giảng viên/Gia sư.
      - **Student:** Học sinh/Sinh viên.
  - **Quản lý Mật khẩu:** Hỗ trợ tính năng "Quên mật khẩu" và "Đặt lại mật khẩu" thông qua Email.

### 🏫 2. Quản lý Đào tạo (Academic Management)

  - **Môn học (Subject):** Quản lý danh mục các môn học.
  - **Lớp học (Class):** Tạo và quản lý lớp học, gán giảng viên, thêm học sinh vào lớp.
  - **Phòng học (Classroom):** Quản lý cơ sở vật chất phòng học.
  - **Hồ sơ:** Xem và cập nhật hồ sơ chi tiết cho Giảng viên và Học sinh (bao gồm kinh nghiệm, đánh giá, mã sinh viên, khóa học...).

### 📅 3. Thời khóa biểu (Scheduling)

  - **Lên lịch:** Tạo lịch học cho từng lớp theo phòng học và khung giờ (Slot).
  - **Lịch lặp lại:** Hỗ trợ tạo lịch học định kỳ (Recurring schedules) cho cả khóa học.
  - **Kiểm tra trùng lịch:** Hệ thống tự động ngăn chặn việc tạo lịch trùng phòng hoặc trùng giờ.
  - **Hỗ trợ Online:** Tích hợp link họp trực tuyến (Meeting Link) vào lịch học.

### 💬 4. Giao tiếp & Tương tác (Communication & Real-time)

  - **Chat Real-time:** Nhắn tin trực tiếp 1-1 giữa Học sinh và Giảng viên (Sử dụng **SignalR**).
  - **Thông báo (Notification):** Nhận thông báo tức thì khi có tin nhắn mới hoặc tương tác mới.
  - **Blog & Bình luận:**
      - Viết và chia sẻ bài viết (Blog) với hỗ trợ tải lên hình ảnh.
      - Tính năng bình luận tương tác dưới bài viết.

### 📊 5. Báo cáo & Thống kê (Dashboard)

  - Xem tổng quan số lượng Học sinh, Giảng viên, Lớp học.
  - Thống kê số buổi học diễn ra trong ngày.
  - Biểu đồ tăng trưởng học sinh mới theo tháng.
  - Bảng xếp hạng Top Giảng viên tiêu biểu.

### 🔍 6. Tìm kiếm (Search)

  - Tìm kiếm thông minh cho phép tra cứu Lớp học và Bài viết (Blog) cùng lúc.

-----

## 🛠️ Công Nghệ Sử Dụng

  - **Core Framework:** ASP.NET Core 8.0 (Web API)
  - **Database:** SQL Server (Entity Framework Core - Code First)
  - **Authentication:** ASP.NET Core Identity & JWT Bearer
  - **Real-time Communication:** SignalR (Hỗ trợ Azure SignalR Service)
  - **Documentation:** Swagger UI / OpenAPI
  - **Email Service:** SMTP (Gmail)
  - **Cloud Ready:** Cấu hình sẵn sàng cho việc triển khai lên Azure App Service.

-----

## ⚙️ Cài Đặt & Chạy Dự Án

### 1\. Yêu cầu hệ thống

  - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  - SQL Server (LocalDB hoặc bản chính thức)
  - Visual Studio 2022 hoặc VS Code

### 2\. Cấu hình Database & AppSettings

Mở tệp `appsettings.json` và cập nhật chuỗi kết nối (`DefaultConnection`), cấu hình JWT và Email:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ProjectComp1640DB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JWT": {
    "Issuer": "http://localhost:5241",
    "Audience": "http://localhost:5241",
    "SigningKey": "YOUR_SUPER_SECRET_KEY_HERE_MUST_BE_LONG_ENOUGH"
  },
  "EmailSettings": {
    "MailServer": "smtp.gmail.com",
    "MailPort": 587,
    "Sender": "your-email@gmail.com",
    "SenderName": "Admin",
    "Password": "your-app-password"
  },
  "Azure": {
    "SignalR": {
      "ConnectionString": "YOUR_AZURE_SIGNALR_CONNECTION_STRING (Optional)"
    }
  }
}
```

### 3\. Chạy Migrations

Mở terminal tại thư mục gốc của dự án và chạy lệnh sau để khởi tạo database:

```bash
dotnet ef database update
```

### 4\. Khởi chạy ứng dụng

Chạy lệnh sau để bắt đầu server:

```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `http://localhost:5241` (hoặc cổng được cấu hình trong `launchSettings.json`).

Truy cập **Swagger UI** để kiểm tra API:
👉 `http://localhost:5241/swagger/index.html`

-----

## 👤 Tài Khoản Admin Mặc Định

Khi chạy ứng dụng lần đầu tiên, hệ thống sẽ tự động tạo một tài khoản Admin mặc định (được định nghĩa trong `Data/DataSeeder.cs`):

  - **Username:** `admin`
  - **Password:** `Admin@123456`

-----

## 📂 Cấu Trúc Dự Án

```
ProjectComp1640/
├── Controllers/       # Các API Endpoints (Account, Class, Blog, etc.)
├── Data/              # DbContext và cấu hình Database
├── Model/             # Các thực thể (Entity) của hệ thống
├── Dtos/              # Data Transfer Objects (DTOs) cho Request/Response
├── Services/          # Logic nghiệp vụ (TokenService, EmailService, etc.)
├── Chat/              # Xử lý Logic Chat và MessageHub
├── NotificationConnect/ # Xử lý thông báo thời gian thực
├── Dashboard/         # Logic thống kê
├── wwwroot/uploads/   # Thư mục chứa hình ảnh upload
└── Program.cs         # Cấu hình khởi chạy ứng dụng & Dependency Injection
```

-----

## 🤝 Đóng Góp

Mọi đóng góp đều được hoan nghênh\! Vui lòng tạo **Pull Request** hoặc mở **Issue** nếu bạn tìm thấy lỗi hoặc muốn đề xuất tính năng mới.

## 📄 License

Dự án này được phát triển cho mục đích học tập và thực hành (ProjectComp1640).
