using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Entitys
{
    [Table("tblRoles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength:250)]
        public string Name { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; } // звязок багато до багатьох з UserProfile

        public Role()
        {
            this.UserProfiles = new HashSet<UserProfile>();
        }
    }
}
