using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1;

namespace BLL.Model
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreate { get; set; }
        //Імена фоток
        public List<ProductImageViewModel> ProductImages { get; set; }
        
        public string FirstImageSmall
        {
            get
            {
                string imageName = "default.jpg";
                string imagePath = Environment.CurrentDirectory +
                    ConfigurationManager.AppSettings["ImageStore"].ToString();
                var image = ProductImages.FirstOrDefault();
                if (image != null)
                    imageName = image.Name;
                return imagePath + "s_" + imageName;
            }
        }


        public string FirstImageOriginal
        {
            get
            {
                string imageName = "default.jpg";
                string imagePath = Environment.CurrentDirectory +
                    ConfigurationManager.AppSettings["ImageStore"].ToString();
                var image = ProductImages.FirstOrDefault();
                if (image != null)
                    imageName = image.Name;
                return imagePath + "o_" + imageName;
            }
        }

    }

    public class ProductImageViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string GetImageSmall
        {
            get
            {
                return Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImageStore"].ToString() + "s_" + Name;
            }
        }

        public string GetImageOriginal
        {
            get
            {
                return Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImageStore"].ToString() + "o_" + Name;
            }
        }
    }

    public class ProductAddViewModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }
        public virtual PhotoCollection Images { get; set; }
    }

}
