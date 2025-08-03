using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config )
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }

        public string CreateToken(AppUser user, List<string> roles)
        {
            var claims = new List<Claim>
            { 
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName,user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var credential = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };           

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

            #region Another way of creating JWT Token

            //var jwtToken = new JwtSecurityToken(
            //    _config["JWT:Issuer"],
            //    _config["JWT:Audience"],
            //    claims,
            //    expires: DateTime.Now.AddDays(1),
            //    signingCredentials: credential
            //    );
            //return new JwtSecurityTokenHandler().WriteToken(jwtToken);

            #endregion
        }
    }
}
