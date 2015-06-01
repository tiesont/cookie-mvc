using System.Configuration;

namespace CookieMVC.Helpers
{
    public class AppSettings
    {
        public static int MinPasswordLength
        {
            get
            {
                //  Recommended minimum password length.
                //  @see https://www.owasp.org/index.php/Password_length_%26_complexity
                int min = 8;

                return GetIntegerValue("app.minPasswordLength",min);
            }
        }

        public static int MaxInvalidPasswordAttempts
        {
            get
            {
                int max = 5;

                return GetIntegerValue("app.maxInvalidPasswordAttempts",max);
            }
        }

        public static int LockedOutWindow
        {
            get
            {
                int window = 10;

                return GetIntegerValue("app.lockedOutWindow",window);
            } 
        }

        public static int PasswordResetWindow
        {
            get
            {
                int window = 60 * 2; // 120 minutes, or 2 hours

                return GetIntegerValue("app.passwordResetWindow", window);
            }
        }

        public static string Name
        {
            get
            {
                return Find("app.name");
            }
        }

        public static string Title
        {
            get
            {
                return Find("app.title");
            }
        }

        public static string Version
        {
            get
            {
                return Find("app.version");
            }
        }

        public static string CopyrightOwner
        {
            get
            {
                return Find("app.owner");
            }
        }

        public static string CopyrightOwnerUrl
        {
            get
            {
                return Find("app.ownerUrl");
            }
        }

        public static string Find(string key)
        {
            // By default, ConfigurationManager.AppSettings[key] would return <c>null</c> if the key isn't found.
            // - @see https://msdn.microsoft.com/en-us/library/8d0bzeeb%28v=vs.110%29.aspx#returns
            // Using the null-coalescing operator (??) allows me to return a default (empty) string instead.
            // - @see https://msdn.microsoft.com/en-us/library/ms173224.aspx
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }


        private static bool GetBoolValue(string key, bool defaultValue)
        {
            var value = Find(key);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;
            bool returnValue;
            if (!bool.TryParse(value, out returnValue))
                return defaultValue;
            return returnValue;
        }

        private static int GetIntegerValue(string key, int defaultValue)
        {
            var value = Find(key);
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;
            int returnValue;
            if (!int.TryParse(value, out returnValue))
                return defaultValue;
            return returnValue;
        }
    }
}
