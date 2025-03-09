namespace ProjectComp1640.Dtos.Account
{
    public class RegisterStudentDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public string StudentCode { get; set; }
        public string Course { get; set; }
        public string Status { get; set; }
    }
}
