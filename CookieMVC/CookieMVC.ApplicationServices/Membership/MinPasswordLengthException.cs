using System;

namespace CookieMVC.ApplicationServices
{
    public class MinPasswordLengthException : Exception
    {
        private int passwordLength;
        private int requiredMinimumLength;

        public MinPasswordLengthException(int passwordLength, int requiredMinimumLength)
            : base(string.Format("Password value must be at least {0} character(s) in length. Value entered was {1} character(s) in length.", requiredMinimumLength, passwordLength))
        {
            this.passwordLength = passwordLength;
            this.requiredMinimumLength = requiredMinimumLength;
        }
    }
}
