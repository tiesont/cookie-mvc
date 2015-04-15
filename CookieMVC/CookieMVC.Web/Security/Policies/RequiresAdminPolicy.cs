using FluentSecurity;
using FluentSecurity.Policy;
//
using CookieMVC.Helpers;

namespace CookieMVC.Web
{
    public class RequiresAdminPolicy : ISecurityPolicy
    {
        public PolicyResult Enforce(ISecurityContext context)
        {
            var innerPolicy = new RequireAnyRolePolicy(RequiredRole.Admin);
            var result = innerPolicy.Enforce(context);

            return result.ViolationOccured ? PolicyResult.CreateFailureResult(this, result.Message) : PolicyResult.CreateSuccessResult(this);
        }
    }
}
