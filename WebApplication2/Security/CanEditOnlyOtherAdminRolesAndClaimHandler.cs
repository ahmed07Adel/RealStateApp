using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication2.Security
{
    public class CanEditOnlyOtherAdminRolesAndClaimHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRqeuire>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRqeuire requirement)
        {
            var authFilterContext = context.Resource as AuthorizationFilterContext;
            if (authFilterContext == null)
            {
                return Task.CompletedTask; 
            }

            string loggedinAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string admininbeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

            if (context.User.IsInRole("admin") && context.User.HasClaim(claim => claim.Type == "EditRole" && claim.Value == "true")&& admininbeingEdited.ToLower() != loggedinAdminId.ToLower())
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

        }
    }
}
