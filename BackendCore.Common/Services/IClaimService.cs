using System;

namespace BackendCore.Common.Services
{
    public interface IClaimService
    {
        Guid UserId { get; }
        string Token { get; }
    }
}
