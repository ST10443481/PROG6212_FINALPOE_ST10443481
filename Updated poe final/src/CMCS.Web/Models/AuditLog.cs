namespace CMCS.Web.Models
{
    public class AuditLog
    {
        public Guid AuditId { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = string.Empty;
        public Guid ActorId { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public string SnapshotJson { get; set; } = "{}";
    }
}
