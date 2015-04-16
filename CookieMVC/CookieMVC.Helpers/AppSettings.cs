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
                //  See https://www.owasp.org/index.php/Password_length_%26_complexity
                int min = 8;

                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["app.minPasswordLength"]))
                {
                    min = int.Parse(ConfigurationManager.AppSettings["app.minPasswordLength"]);
                }

                return min;
            }
        }

        public static int MaxInvalidPasswordAttempts
        {
            get
            {
                int max = 5;

                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["app.maxInvalidPasswordAttempts"]))
                {
                    max = int.Parse(ConfigurationManager.AppSettings["app.maxInvalidPasswordAttempts"]);
                }

                return max;
            }
        }

        public static int LockedOutWindow
        {
            get
            {
                int window = 10;

                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["app.lockedOutWindow"]))
                {
                    window = int.Parse(ConfigurationManager.AppSettings["app.lockedOutWindow"]);
                }

                return window;
            } 
        }

        public static int PasswordResetWindow
        {
            get
            {
                int window = 60 * 2; // 120 minutes, or 2 hours

                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["app.passwordResetWindow"]))
                {
                    window = int.Parse(ConfigurationManager.AppSettings["app.passwordResetWindow"]);
                }

                return window;
            }
        }

        public static string Name
        {
            get
            {
                return ConfigurationManager.AppSettings["app.name"];
            }
        }

        public static string Title
        {
            get
            {
                return ConfigurationManager.AppSettings["app.title"];
            }
        }

        public static string Version
        {
            get
            {
                return ConfigurationManager.AppSettings["app.version"];
            }
        }

        public static string CopyrightOwner
        {
            get
            {
                return ConfigurationManager.AppSettings["app.owner"];
            }
        }

        public static string CopyrightOwnerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["app.ownerUrl"];
            }
        }

        public static string Find(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }


        private static bool GetBoolValue(string key, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return defaultValue;
            bool returnValue;
            if (!bool.TryParse(value, out returnValue))
                return defaultValue;
            return returnValue;
        }
    }
}
