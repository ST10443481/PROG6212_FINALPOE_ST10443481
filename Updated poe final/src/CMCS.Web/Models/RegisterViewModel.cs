using System.ComponentModel.DataAnnotations;

namespace CMCS.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        public string ConfirmPassword { get; set; } = "";

        [Required]
        public string Role { get; set; } = "Lecturer";

        public decimal HourlyRate { get; set; }
    }
}
