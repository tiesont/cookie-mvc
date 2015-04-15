using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using CookieMVC.ApplicationServices;
//
using FluentSecurity;
using FluentSecurity.Configuration;

namespace CookieMVC.Web.Security
{
    public partial class SecurityProvider
    {
        public static bool ActionIsAllowedForUser(string controllerName, string actionName)
        {
            var configuration = SecurityConfiguration.Current;
            var policyContainer = configuration.Runtime.PolicyContainers.GetContainerFor(controllerName, actionName);
            if (policyContainer != null)
            {
                var results = policyContainer.EnforcePolicies(FluentSecurity.SecurityContext.Current);
                return results.All(x => x.ViolationOccured == false);
            }

            // Assume for now that no policies means no restrictions
            return true;
        }

        public static bool HasAnyRole(params string[] roles)
        {
            bool hasAnyRole = false;
            if (IsUserAuthenticated())
            {
                var user = HttpContext.Current.User;
                var services = DependencyResolver.Current.GetService<IMembershipService>();

                hasAnyRole = services.UserHasAnyRole(user.Identity.Name, roles);
            }

            return hasAnyRole;
        }


        public static IEnumerable<string> GetCurrentUserRoles()
        {
            IEnumerable<string> roles = null;
            if (IsUserAuthenticated())
            {
                var user = HttpContext.Current.User;
                var services = DependencyResolver.Current.GetService<IMembershipService>();

                roles = services.GetRolesForUser(user.Identity.Name);
            }

            return roles;
        }

        public static bool IsUserAuthenticated()
        {
            bool isAuthenticated = false;
            var user = HttpContext.Current.User;
            if (user != null && user.Identity != null)
            {
                isAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            }

            return isAuthenticated;
        }
    }
}