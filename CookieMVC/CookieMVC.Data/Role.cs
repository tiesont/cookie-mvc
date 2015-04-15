using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieMVC.Data
{
    public partial class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
