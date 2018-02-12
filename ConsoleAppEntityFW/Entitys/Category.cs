using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Entitys
{
    [Table("tblCategories")]
    public class Category
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [ForeignKey("Parent"), Column(Order = 1)]
        public int? ParentId { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }

        public bool IsHead { get; set; }

        public ICollection<Category> Parent { get; set; }

        public ICollection<Product> Products { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
