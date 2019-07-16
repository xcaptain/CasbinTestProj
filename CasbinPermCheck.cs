using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using NetCasbin;

namespace CasbinTestProj
{
    public class CasbinCheckPermRequirement : IAuthorizationRequirement
    {

    }

    public class CasbinCheckPermHandler : AuthorizationHandler<CasbinCheckPermRequirement>
    {
        private readonly Enforcer _enforcer;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CasbinCheckPermHandler(Enforcer enforcer, IHttpContextAccessor httpContextAccessor)
        {
            _enforcer = enforcer;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                CasbinCheckPermRequirement requirement)
        {
            // you can get current user id from claim for example:
            // _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.ToString() == "UserId").Value;
            if (!_enforcer.Enforce("user_1", _httpContextAccessor.HttpContext.Request.Path.ToString(), _httpContextAccessor.HttpContext.Request.Method.ToString()))
            {
                context.Fail();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
