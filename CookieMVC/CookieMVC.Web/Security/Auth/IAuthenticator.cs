
namespace CookieMVC.Web.Security
{
    public interface IAuthenticator
    {
        void SignIn(string userName, bool createPersistantLogin = false);
        void SignOut();
    }
}
