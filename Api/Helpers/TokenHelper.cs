using Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Api.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789 a very long word"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                 new Claim("userId", user.id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, user.name),
                 new Claim(JwtRegisteredClaimNames.Email, user.mail),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken("123456789 a very long word",
            "123456789 a very long word",
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("123456789 a very long word");
            try
            {
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);

            }
            catch (SecurityTokenException ex)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }

}