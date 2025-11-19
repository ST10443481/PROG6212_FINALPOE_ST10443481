namespace CMCS.Web.Models
{
    public class Document
    {
        public Guid DocumentId { get; set; }
        public Guid ClaimId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = "pdf";
        public string BlobPath { get; set; } = "/blob/path";
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
