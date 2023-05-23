using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AutoPark.Api.Authentication
{
    public class IsManagerPolicyHandler : AuthorizationHandler<IsManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsManagerRequirement requirement)
        {
            var claims = context.User.Claims;
            var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type == "role");

            if (roleClaim?.Value == "manager")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class IsManagerRequirement : IAuthorizationRequirement
    {
    }
}