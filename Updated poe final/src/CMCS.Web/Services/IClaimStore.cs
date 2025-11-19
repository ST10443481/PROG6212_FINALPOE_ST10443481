using CMCS.Web.Models;

namespace CMCS.Web.Services
{
    public interface IClaimStore
    {
        IEnumerable<Claim> All();
        IEnumerable<Claim> Pending();
        Claim? Get(string id);
        Claim Add(Claim claim);
        void Approve(string id);
        void Reject(string id);
        void Settle(string id);
    }
}
