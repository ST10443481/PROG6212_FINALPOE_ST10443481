// File-scoped namespace for brevity
namespace UnitTestProject1;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMCS.Web.Models;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Amount_Computes_Correctly()
    {
        var c = new Claim { HoursWorked = 10m, HourlyRate = 250m };
        Assert.AreEqual(2500m, c.Amount, "HoursWorked * HourlyRate should compute Amount.");
    }

    [TestMethod]
    public void Default_Status_Is_PendingVerification()
    {
        var c = new Claim();
        Assert.AreEqual(ClaimStatus.PendingVerification, c.Status, "New claims should default to PendingVerification.");
    }
}
