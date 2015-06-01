using System;
using System.Collections.Generic;

namespace CookieMVC.ApplicationServices
{
    public interface IMembershipService : IDisposable
    {
        bool CredentialsAreValid(string userName, string password);

        MembershipStatus GetMembershipStatus(string userName);

        string GeneratePasswordResetToken(string userName);
        bool ResetTokenIsValid(string token);
        bool ResetPassword(string token, string password);
        bool UpdatePasswordForUser(string userName, string oldPassword, string newPassword);

        bool UserIsInRole(string userName, string roleName);
        bool UserHasAnyRole(string userName, params string[] roles);
        bool UserHasAllRoles(string userName, params string[] roles);
        IEnumerable<string> GetRolesForUser(string userName);

        bool LockUser(string userName);
        bool UnlockUser(string userName);

        bool UserNameIsAvailable(string userName);
    }
}
