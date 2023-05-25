using System;
using System.Collections.Generic;
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
            var mySecurityKey = new SymmetricSecurityKey(keyBytes);
            var myIssuer = "autopark";
            var myAudience = "test";
            var tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> roleClaims = new List<Claim>();
            foreach (var role in loginModel.Roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            tokenDescriptor.Subject.AddClaims(roleClaims);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}