using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Entitys
{
    [Table("tblCarts")]
    public class Cart
    {
        [Key, ForeignKey("UserProfileOf")]
        public int UserProfileId { get; set; }

        public DateTime DateCreate { get; set; } // якщо поле не обовязкове public DateTime ? DateCreate { get; set; }

        public virtual UserProfile UserProfileOf { get; set; }
    }
}
