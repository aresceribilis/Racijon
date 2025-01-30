using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Racijon.AuthService
{
    public class JwtService(IOptions<JwtOptions> jwtOptions)
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public string GenerateToken(Tuple<string, string, Guid> tokenModel)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, tokenModel.Item3.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nickname, tokenModel.Item2)
            };

            if (!string.IsNullOrEmpty(tokenModel.Item1))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, tokenModel.Item1));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}