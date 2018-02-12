using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;

namespace BLL.Model
{
    public class ProductAddViewModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public virtual PhotoCollection Images { get; set; }
    }
}
