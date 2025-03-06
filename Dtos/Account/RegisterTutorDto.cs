namespace ProjectComp1640.Dtos.Account
{
    public class RegisterTutorDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public int? ExperienceYears { get; set; }
        public float? Rating { get; set; }
    }
}
