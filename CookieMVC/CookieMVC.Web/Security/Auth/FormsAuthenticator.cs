using System.Web.Security;

namespace CookieMVC.Web.Security
{
    public class FormsAuthenticator : IAuthenticator
    {
        public void SignIn(string userName, bool createPersistantLogin = false)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistantLogin);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}