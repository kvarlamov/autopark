using Microsoft.AspNetCore.Authentication;

namespace AutoPark.Api.Authentication
{
    public class JwtSchemeOptions : AuthenticationSchemeOptions
    {
        public bool IsActive { get; set; }
    }
}