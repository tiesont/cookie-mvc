using System.ComponentModel;

namespace CookieMVC.ApplicationServices
{
    public enum MembershipStatus
    {
        [Description("Unknown")]
        Unknown = 1,

        [Description("Not registered")]
        NotRegistered = 2,

        [Description("Active")]
        Active = 3,

        [Description("Not active")]
        NotActive = 4,

        [Description("Locked")]
        Locked = 5
    }
}
