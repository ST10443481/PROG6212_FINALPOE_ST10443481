public interface IClaimStore
{
    IEnumerable<Claim> All();
    IEnumerable<Claim> Pending();
    Claim? Get(Guid id);  // Change 'string' to 'Guid'
    Claim Add(Claim claim);
    void Approve(Guid id); // Change 'string' to 'Guid'
    void Reject(Guid id);  // Change 'string' to 'Guid'
    void Settle(Guid id);  // Change 'string' to 'Guid'
    void Update(Claim claim);
}
