using System.Collections.Concurrent;
using CMCS.Web.Models;

namespace CMCS.Web.Services
{
    public class InMemoryClaimStore : IClaimStore
    {
        private readonly ConcurrentDictionary<string, Claim> _db = new();

        public IEnumerable<Claim> All() => _db.Values.OrderByDescending(c => c.CreatedAt);
        public IEnumerable<Claim> Pending() => _db.Values.Where(c => c.Status == ClaimStatus.PendingVerification);

        public Claim? Get(string id) => id != null && _db.TryGetValue(id, out var c) ? c : null;

        public Claim Add(Claim claim)
        {
            _db[claim.Id] = claim;
            return claim;
        }

        public void Approve(string id)
        {
            if (Get(id) is { } c) { c.Status = ClaimStatus.Approved; c.UpdatedAt = DateTime.UtcNow; }
        }

        public void Reject(string id)
        {
            if (Get(id) is { } c) { c.Status = ClaimStatus.Rejected; c.UpdatedAt = DateTime.UtcNow; }
        }

        public void Settle(string id)
        {
            if (Get(id) is { } c) { c.Status = ClaimStatus.Settled; c.UpdatedAt = DateTime.UtcNow; }
        }
    }
}
