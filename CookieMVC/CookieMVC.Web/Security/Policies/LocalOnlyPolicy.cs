using System.Web;

using FluentSecurity;
using FluentSecurity.Policy;

namespace CookieMVC.Web
{
    public class LocalOnlyPolicy : ISecurityPolicy
    {
        public PolicyResult Enforce(ISecurityContext context)
        {
            return HttpContext.Current.Request.IsLocal ? PolicyResult.CreateSuccessResult(this) : PolicyResult.CreateFailureResult(this, "This resource cannot be remotely accessed.");
        }
    }
}
