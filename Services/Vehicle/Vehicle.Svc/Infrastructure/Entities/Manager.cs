using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AutoPark.Svc.Infrastructure.Entities
{
    public class Manager : IdentityUser<long>
    {
        public List<Enterprise> Enterprises { get; set; } = new();
    }
}