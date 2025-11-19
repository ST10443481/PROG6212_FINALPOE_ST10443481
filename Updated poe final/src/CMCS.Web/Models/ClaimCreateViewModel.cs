using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CMCS.Web.Models
{
    public class ClaimCreateViewModel
    {
        [Display(Name = "Lecturer")]
        public Guid? LecturerId { get; set; }

        [Required, Range(0.5, 400), Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Required, Range(0, 10000), Display(Name = "Hourly Rate (R)")]
        public decimal HourlyRate { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Display(Name = "Supporting Document (.pdf, .docx, .xlsx, ≤10 MB)")]
        public IFormFile? Document { get; set; }
    }
}
