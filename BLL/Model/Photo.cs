using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL.Model
{
    public class Photo
    {
        private string _path;
        private string _pathOriginal; // путь к оригинальной фотке
        //private Uri _source;
        private BitmapFrame _image;

        public Photo() { }
        public Photo(string path)
        {
            if(!(Directory.Exists(Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString())))
                    Directory.CreateDirectory(Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString());
            // 
            _pathOriginal = path;
            //_path = "L:\\Temp\\" + Guid.NewGuid().ToString() + ".jpg";
            _path = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString() + Guid.NewGuid().ToString() + ".jpg";
            using (var image = System.Drawing.Image.FromFile(_pathOriginal))
            {
                var newImageSmall = ImageWorker.ConverImageToBitmap(image, 130, 130);
                if (newImageSmall != null)
                {
                    newImageSmall.Save(_path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    _image = BitmapFrame.Create(new Uri(_path));
                }
                //_source = new Uri(path);
            }
        }

        public Photo(ProductImage path) // конструктор получения изображений из базы
        {
            _path = path.NameSmall;
            _pathOriginal = path.NameOriginal;
        }

        //public override string ToString()
        //{
        //    return _source.ToString();
        //}
        public string Source { get { return _path; } } // путь на уменьшенную фотку

        public string SourceOriginal { get { return _pathOriginal; } } // путь на оригинальную фотку

        public BitmapFrame Image { get { return _image; } set { _image = value; } }

        public string ImageName { get { return Path.GetFileNameWithoutExtension(_pathOriginal); } }


    }
}
