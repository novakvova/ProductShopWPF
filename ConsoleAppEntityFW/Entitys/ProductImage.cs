using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Entitys
{
    [Table("tblProductImages")]
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength:150)]
        public string Name { get; set; }

        //public bool Priority { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
