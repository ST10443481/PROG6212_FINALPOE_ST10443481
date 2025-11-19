namespace CMCS.Web.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = "";
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Lecturer"; // Lecturer|Coordinator|Manager|HR
        public bool IsActive { get; set; } = true;


        // NEW: hourly rate in Rands
        public decimal HourlyRate { get; set; } = 0m;
    }
}