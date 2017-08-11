using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TechTime.Models;

namespace TechTime.Authorization
{
    public class OwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, JobEntry>
    {
        private UserManager<UserLogin> _userManager;

        public OwnerAuthorizationHandler(UserManager<UserLogin> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, 
            JobEntry resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.FromResult(0);
            }

            if (requirement.Name != Constants.EditDescOperation)
            {
                return Task.FromResult(0);
            }

            if (resource.OwnerId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
