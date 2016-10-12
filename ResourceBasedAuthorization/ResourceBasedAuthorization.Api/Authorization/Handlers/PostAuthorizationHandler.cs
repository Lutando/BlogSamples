using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using ResourceBasedAuthorization.Api.Authorization.Models;

namespace ResourceBasedAuthorization.Api.Authorization.Handlers
{
    public class PostAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PostAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, PostAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "PostWrite")
            {
                if (context.User.HasClaim("username", resource.Username))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }
            //if they want to do a PostDelete, we check to see if they own the resource or if they are a moderator
            if (requirement.Name == "PostDelete")
            {
                if (context.User.HasClaim("username", resource.Username) || context.User.HasClaim("role","moderator"))
                {
                    //if the principle satisfies our requirements we tell the context that we are good to go
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            if (requirement.Name == "PostEdit")
            {
                if (context.User.HasClaim("username", resource.Username) || context.User.HasClaim("role", "moderator"))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            //if no requirements have been met we do nothing to the context, and this is one way to implicitly
            //tell the context that we want to challenge the request in this context.
            return noOp;
        }
    }
}
