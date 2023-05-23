using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoPark.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace AutoPark.Api.Authentication
{
    public class JwtService
    {
        //todo - убрать в JwtSchemeOptions!!! НЕБЕЗОПАСНО
        private const string SecretKey = "this is my custom Secret key for authentication";
        public static string GenerateRefreshToken()
        {
            return string.Empty;
        }

        public static string CreateJwtToken(LoginViewModel loginModel)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(SecretKey);
            // Array.Resize(ref keyBytes, 32);
            var mySecurityKey = new SymmetricSecurityKey(keyBytes);
            var myIssuer = "autopark";
            var myAudience = "test";
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.UserName),
                    new Claim(ClaimTypes.Role, loginModel.Roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}