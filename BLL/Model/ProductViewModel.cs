using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1;

namespace BLL.Model
{
    public class ProductItemViewModel
    {
        private BitmapFrame _imageSmall;
        private BitmapFrame _imageOriginal;
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreate { get; set; }
        //Імена фоток
        public List<ProductImageViewModel> ProductImages { get; set; }
        
        public BitmapFrame FirstImageSmall
        {
            get
            {
                string imageName = "default.jpg";
                string imagePath = Environment.CurrentDirectory +
                    ConfigurationManager.AppSettings["ImageStore"].ToString();
                var image = ProductImages.FirstOrDefault();
                if (image != null)
                    imageName = image.Name;
                string pathInput = imagePath + "s_" + imageName;
                using (var imageNew = System.Drawing.Image.FromFile(pathInput))
                {
                    var newImageSmall = ImageWorker.ConverImageToBitmap(imageNew, 130, 130);
                    if (newImageSmall != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            newImageSmall.Save(ms, ImageFormat.Bmp);
                            _imageSmall = BitmapFrame.Create(ms,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        }
                    }
                }
                return _imageSmall;
            }
        }


        public BitmapFrame FirstImageOriginal
        {
            get
            {
                string imageName = "default.jpg";
                string imagePath = Environment.CurrentDirectory +
                    ConfigurationManager.AppSettings["ImageStore"].ToString();
                var image = ProductImages.FirstOrDefault();
                if (image != null)
                    imageName = image.Name;
                string pathInput = imagePath + "o_" + imageName;
                using (var imageNew = System.Drawing.Image.FromFile(pathInput))
                {
                    var newImageOrigin = ImageWorker.ConverImageToBitmap(imageNew, imageNew.Width, imageNew.Height);
                    if (newImageOrigin != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            newImageOrigin.Save(ms, ImageFormat.Bmp);
                            _imageOriginal = BitmapFrame.Create(ms,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        }
                    }
                }
                return _imageOriginal;
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
