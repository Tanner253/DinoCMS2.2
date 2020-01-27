using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Models.Handlers
{
    public class PrStaffHandler
    {
        protected Task HandleRequirementAsync(AuthorizationHandlerContext context, PrStaff requirement)
        {
            if(!context.User.HasClaim(c => c.Type == "PrStaff"))
            {
                return Task.CompletedTask;
            }
            var PrStaff = context.User.FindFirst(c => c.Type == "PrStaff").Value;

            if(PrStaff == requirement.laRequirement)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
