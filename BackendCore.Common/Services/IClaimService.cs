
namespace BackendCore.Common.Services
{
    public interface IClaimService
    {
        long UserId { get; }
        string Token { get; }
    }
}
