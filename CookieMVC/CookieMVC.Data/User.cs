using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieMVC.Data
{
    public partial class User
    {
        public User()
        {
            this.Roles = new HashSet<Role>();
        }

        [Key]
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int PasswordAttempts { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? LockedOutOn { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiresOn { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
