using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieTheaterApi.JWT
{
    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettigns;
        private readonly UserManager<User> _userManager;

        public JwtHandler(IConfiguration configuration,
            UserManager<User> userManager)
        {
            _configuration = configuration;
            _jwtSettigns = _configuration.GetSection("Jwt");
            _userManager = userManager;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettigns.GetSection("Key").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }

        public List<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            return claims;
        }

        public JwtSecurityToken GenerateToken(SigningCredentials signingCredentials,
            List<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtSettigns.GetSection("Issuer").Value,
                audience: _jwtSettigns.GetSection("Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials);

            return token;
        }
    }
}
