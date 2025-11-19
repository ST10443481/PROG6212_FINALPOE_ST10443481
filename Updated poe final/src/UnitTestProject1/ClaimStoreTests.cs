// File-scoped namespace
namespace UnitTestProject1;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMCS.Web.Models;
using CMCS.Web.Services;
using System.Linq;

[TestClass]
public class ClaimStoreTests
{
    [TestMethod]
    public void Add_And_Get_Assigns_Id_And_Persists()
    {
        var store = new InMemoryClaimStore();
        var c = new Claim { Lecturer = "L1", HoursWorked = 2m, HourlyRate = 100m, Status = ClaimStatus.Submitted };
        var saved = store.Add(c);
        Assert.IsFalse(string.IsNullOrWhiteSpace(saved.Id));
        var fetched = store.Get(saved.Id);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(200m, fetched!.Amount);
        Assert.AreEqual(ClaimStatus.Submitted, fetched.Status);
    }

    [TestMethod]
    public void Pending_Returns_Only_PendingVerification()
    {
        var store = new InMemoryClaimStore();
        store.Add(new Claim { Status = ClaimStatus.PendingVerification, HoursWorked = 1m, HourlyRate = 50m });
        store.Add(new Claim { Status = ClaimStatus.Approved, HoursWorked = 1m, HourlyRate = 50m });
        var pending = store.Pending().ToList();
        Assert.AreEqual(1, pending.Count);
        Assert.AreEqual(ClaimStatus.PendingVerification, pending[0].Status);
    }

    [TestMethod]
    public void Approve_Reject_Settle_Transitions_And_Are_Idempotent_For_Missing_Id()
    {
        var store = new InMemoryClaimStore();
        var c = store.Add(new Claim { Status = ClaimStatus.Submitted, HoursWorked = 1m, HourlyRate = 10m });

        // Valid transitions
        store.Approve(c.Id);
        Assert.AreEqual(ClaimStatus.Approved, store.Get(c.Id)!.Status);

        store.Reject(c.Id);
        Assert.AreEqual(ClaimStatus.Rejected, store.Get(c.Id)!.Status);

        store.Settle(c.Id);
        Assert.AreEqual(ClaimStatus.Settled, store.Get(c.Id)!.Status);

        // Graceful no-ops for missing ids (should not throw)
        store.Approve("does-not-exist");
        store.Reject("does-not-exist");
        store.Settle("does-not-exist");
        // Still no exceptions, state unchanged
        Assert.AreEqual(ClaimStatus.Settled, store.Get(c.Id)!.Status);
    }
}
