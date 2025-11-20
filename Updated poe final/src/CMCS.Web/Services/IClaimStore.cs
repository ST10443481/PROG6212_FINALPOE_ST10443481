using System;
using System.Collections.Generic;
using CMCS.Web.Models;

namespace CMCS.Web.Services
{
    public interface IClaimStore
    {
        Claim Get(Guid claimId);
        List<Claim> All();
        void Approve(Guid claimId);
        void Reject(Guid claimId);
        void Settle(Guid claimId);
        void Update(Claim claim);
        void Add(Claim claim);
    }
}