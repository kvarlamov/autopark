using System.Collections.Generic;

namespace AutoPark.Api.Models
{
    public class LoginDto
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}