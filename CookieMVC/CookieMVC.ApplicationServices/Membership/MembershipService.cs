using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CookieMVC.Helpers;
using CookieMVC.Security;

namespace CookieMVC.ApplicationServices
{
    public class MembershipService : ServiceBase, IMembershipService
    {
        private readonly int iterations = 2000;

        public bool CredentialsAreValid(string userName, string password)
        {
            bool valid = false;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                if (user.IsActive && (user.LockedOn == null || user.LockedOn.Value.AddMinutes(AppSettings.LockedOutWindow) < DateTime.Now))
                {
                    if (PasswordUtility.VerifyHashedPassword(user.Password, user.Salt + password, iterations))
                    {
                        valid = true;
                        if (user.PasswordAttempts > 0 || (user.LockedOn != null && user.LockedOn.Value.AddMinutes(AppSettings.LockedOutWindow) < DateTime.Now))
                        {
                            UnlockUser(userName);
                        }
                    }
                    else
                    {
                        user.PasswordAttempts++;
                        if (user.PasswordAttempts > AppSettings.MaxInvalidPasswordAttempts)
                        {
                            LockUser(userName);
                        }
                        else
                        {
                            Context.SaveChanges(true);
                        }
                    }
                }
            }

            return valid;
        }

        public MembershipStatus GetMembershipStatus(string userName)
        {
            MembershipStatus statusCode = MembershipStatus.NotRegistered;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                if (user.IsActive)
                {
                    if (user.LockedOn != null)
                    {
                        statusCode = MembershipStatus.Locked;
                    }
                    else
                    {
                        statusCode = MembershipStatus.Active;
                    }
                }
                else
                {
                    statusCode = MembershipStatus.NotActive;
                }
            }

            return statusCode;
        }

        public string GeneratePasswordResetToken(string userName)
        {
            string token = null;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                token = HashUtility.GenerateToken();
                user.ResetToken = token;
                user.ResetTokenExpiresOn = DateTime.Now.AddHours(AppSettings.PasswordResetWindow);

                Context.SaveChanges(true);
            }

            return token;
        }

        public bool ResetTokenIsValid(string token)
        {
            return Context.Users.Any(u => u.ResetToken.Equals(token, StringComparison.Ordinal));
        }

        public bool ResetPassword(string token, string password)
        {
            bool success = false;

            if (password.Length >= AppSettings.MinPasswordLength)
            {
                var user = Context.Users.SingleOrDefault(u => u.ResetToken.Equals(token, StringComparison.Ordinal));
                if (user != null && user.ResetTokenExpiresOn > DateTime.Now)
                {
                    user.Salt = HashUtility.GenerateSalt();
                    user.Password = PasswordUtility.HashPassword(user.Salt + password, iterations);
                    user.ResetToken = null;
                    user.ResetTokenExpiresOn = null;

                    user.UpdatedOn = DateTime.Now;

                    success = Context.Entry<Data.User>(user).State == System.Data.Entity.EntityState.Unchanged || Context.SaveChanges(true) > 0;
                }
            }
            else
            {
                throw new MinPasswordLengthException(password.Length, AppSettings.MinPasswordLength);
            }

            return success;
        }

        public bool UpdatePasswordForUser(string userName, string oldPassword, string newPassword)
        {
            bool success = false;

            if (newPassword.Length >= AppSettings.MinPasswordLength)
            {
                var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    if (PasswordUtility.VerifyHashedPassword(user.Password, oldPassword, iterations))
                    {
                        user.Salt = HashUtility.GenerateSalt();
                        user.Password = PasswordUtility.HashPassword(user.Salt + newPassword, iterations);
                        user.UpdatedOn = DateTime.Now;

                        success = Context.SaveChanges(true) > 0;
                    }
                }
            }
            else
            {
                throw new MinPasswordLengthException(newPassword.Length, AppSettings.MinPasswordLength);
            }

            return success;
        }

        public bool UserIsInRole(string userName, string roleName)
        {
            bool matchFound = false;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                matchFound = user.Roles.Any(role => role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            }

            return matchFound;
        }

        public bool UserHasAnyRole(string userName, params string[] roles)
        {
            bool matchFound = false;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                matchFound = user.Roles.Any(role => roles.Contains(role.Name));
            }

            return matchFound;
        }

        public bool UserHasAllRoles(string userName, params string[] roles)
        {
            bool matchFound = false;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                matchFound = user.Roles.All(role => roles.Contains(role.Name));
            }

            return matchFound;
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            return user == null ? null : user.Roles.Select(role => role.Name).ToArray();
        }

        public bool LockUser(string userName)
        {
            bool success = false;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                var today = DateTime.Now;
                user.LockedOn = today;
                user.UpdatedOn = today;

                success = Context.SaveChanges(true) > 0;
            }

            return success;
        }

        public bool UnlockUser(string userName)
        {
            bool success = false;
            var user = Context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                user.PasswordAttempts = 0;
                user.LockedOn = null;
                user.UpdatedOn = DateTime.Now;

                success = Context.SaveChanges(true) > 0;
            }

            return success;
        }

        public bool UserNameIsAvailable(string userName)
        {
            return !Context.Users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
