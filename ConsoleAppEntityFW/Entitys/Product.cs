using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Entitys
{
    [Table("tblProducts")]
    public class Product
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required, StringLength(maximumLength:250)]
        public string Name { get; set; }

        [ForeignKey("Categories"), Column(Order = 1)]
        public int CategoryId { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreate { get; set; }

        public ICollection<Filter> Filtres { get; set; }

        public virtual Category Categories { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        [NotMapped]
        public string FirstImageSmall => ProductImages.FirstOrDefault().NameSmall;

        [NotMapped]
        public string FirstImageOriginal => ProductImages.FirstOrDefault().NameOriginal;

        public Product()
        {
            Category cat = new Category();
        }



    }
}
