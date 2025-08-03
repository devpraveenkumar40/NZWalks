using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, List<string> roles);
    }
}
