namespace Common.Helpers.TokenGenerator
{   
    public interface ITokenGenerator
    {
        string GenerateRestToken(string email);
        bool ValidateToken(string token);
        string DecodeToken(string token);
    }
}
