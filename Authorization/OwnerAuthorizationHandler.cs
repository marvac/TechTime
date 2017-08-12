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

            //Don't allow user to set payment status

            if (resource.OwnerId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
