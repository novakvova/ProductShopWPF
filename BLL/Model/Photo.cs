using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
            _pathOriginal = path;
           using (var image = System.Drawing.Image.FromFile(_pathOriginal))
            {
                var newImageSmall = ImageWorker.ConverImageToBitmap(image, 130, 130);
                if (newImageSmall != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        newImageSmall.Save(ms,ImageFormat.Bmp);
                        _image = BitmapFrame.Create(ms, 
                            BitmapCreateOptions.PreservePixelFormat, 
                            BitmapCacheOption.OnLoad);
                    }
                }
            }
        }
        
        public Photo(ProductImageViewModel path) // конструктор получения изображений из базы
        {
            _path = path.GetImageSmall;
            _pathOriginal = path.GetImageOriginal;
        }
        
        public string Source { get { return _path; } } // путь на уменьшенную фотку

        public string SourceOriginal { get { return _pathOriginal; } } // путь на оригинальную фотку

        public BitmapFrame ImageFrame { get { return _image; } set { _image = value; } }

        public string ImageName { get { return Path.GetFileNameWithoutExtension(_pathOriginal); } }


    }
}
