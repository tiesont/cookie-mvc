using System;

namespace CookieMVC.Web
{
    [Serializable]
    public class NullMailerException : Exception
    {
        public NullMailerException()
            : this("Email service has not been configured, or has been disposed. Mail could not be sent.")
        {
        }

        public NullMailerException(string message)
            : base(message)
        {
        }
    }
}