using System;
using System.Collections.Generic;
using System.Linq;
using CMCS.Web.Services;
public class InMemoryClaimStore : IClaimStore
{
    // In-memory storage for claims
    private List<Claim> claims = new List<Claim>();

    // Implementing the Get method to retrieve a claim by its unique ID
    public Claim Get(Guid claimId)
    {
        return claims.FirstOrDefault(c => c.Id == claimId);
    }

    // Implementing the All method to retrieve all claims
    public List<Claim> All()
    {
        return claims;
    }

    // Implementing the Approve method to approve a claim
    public void Approve(Guid claimId)
    {
        var claim = Get(claimId);
        if (claim != null)
        {
            claim.Status = "Approved";  // Set the status to Approved
            // Other logic for approving the claim could be added here
        }
    }

    // Implementing the Reject method to reject a claim
    public void Reject(Guid claimId)
    {
        var claim = Get(claimId);
        if (claim != null)
        {
            claim.Status = "Rejected";  // Set the status to Rejected
            // Other logic for rejecting the claim could be added here
        }
    }

    // Implementing the Settle method to settle a claim
    public void Settle(Guid claimId)
    {
        var claim = Get(claimId);
        if (claim != null)
        {
            claim.Status = "Settled";  // Set the status to Settled
            // Other logic for settling the claim could be added here
        }
    }

    // Implementing the Update method to update an existing claim
    public void Update(Claim claim)
    {
        var existingClaim = Get(claim.Id);
        if (existingClaim != null)
        {
            // Update the claim properties
            existingClaim.Status = claim.Status; // Example: updating status
            existingClaim.Description = claim.Description; // Example: updating description
            // Add other properties you want to update
        }
    }

    // Implementing the Add method to add a new claim
    public void Add(Claim claim)
    {
        claims.Add(claim);  // Add the new claim to the in-memory list
    }
}
