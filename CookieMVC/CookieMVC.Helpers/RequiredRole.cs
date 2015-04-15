namespace CookieMVC.Helpers
{
    public class RequiredRole
    {
        public static string Admin = "Admin";
        public static string Member = "Member";

        public static string[] Any
        {
            get
            {
                return new string[] { Admin, Member };
            }
        }
    }
}
