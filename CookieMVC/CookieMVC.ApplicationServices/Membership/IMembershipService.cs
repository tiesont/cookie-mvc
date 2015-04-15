using System;
using System.Collections.Generic;

namespace CookieMVC.ApplicationServices
{
    public interface IMembershipService : IDisposable
    {
        bool ValidateCredentials(string userName, string password);

        MembershipStatus CheckMembershipStatus(string userName);

        string GeneratePasswordResetToken(string userName);
        bool IsResetTokenValid(string token);
        bool ResetPassword(string token, string password);
        bool UpdatePasswordForUser(string userName, string oldPassword, string newPassword);

        bool IsUserInRole(string userName, string roleName);
        bool UserHasAnyRole(string userName, params string[] roles);
        bool UserHasAllRoles(string userName, params string[] roles);
        IEnumerable<string> GetRolesForUser(string userName);

        bool LockUser(string userName);
        bool UnLockUser(string userName);

        bool IsUserNameAvailable(string userName);
    }
}
