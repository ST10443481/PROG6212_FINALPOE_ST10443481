
using CMCS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace CMCS.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IClaimStore _store;
        public ReportsController(IClaimStore store)
        {
            _store = store;
        }

        // GET /Reports/ApprovedClaimsCsv
        public IActionResult ApprovedClaimsCsv()
        {
            var approved = _store.All().Where(c => c.Status == CMCS.Web.Models.ClaimStatus.Approved);
            var sb = new StringBuilder();
            sb.AppendLine("Id,Lecturer,HoursWorked,HourlyRate,Total,Status,SubmittedAt");
            foreach(var c in approved)
            {
                var total = (c.HoursWorked * c.HourlyRate).ToString();
                sb.AppendLine($"{c.Id},{c.Lecturer},{c.HoursWorked},{c.HourlyRate},{total},{c.Status},{c.CreatedAt:O}");
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "approved_claims.csv");
        }
    }
}
