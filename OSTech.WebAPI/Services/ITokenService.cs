using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OSTech.WebAPI.Services
{
    public interface ITokenService 
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);
        string GenerateRefrestToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
    }
}
