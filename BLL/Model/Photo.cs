﻿using ConsoleAppEntityFW.Entitys;
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
        private BitmapFrame _imageOrigin;

        public Photo() { }
        public Photo(string path)
        { 
            _pathOriginal = path;
           using (var image = System.Drawing.Image.FromFile(_pathOriginal))
            {
                using (var newImageSmall = ImageWorker.ConverImageToBitmap(image, 130, 130))
                {
                    if (newImageSmall != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            newImageSmall.Save(ms, ImageFormat.Bmp);
                            _image = BitmapFrame.Create(ms,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        }
                    }
                }
                using (var newImageOrigin = ImageWorker.ConverImageToBitmap(image, image.Width, image.Height))
                {
                    if (newImageOrigin != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            newImageOrigin.Save(ms, ImageFormat.Bmp);
                            _imageOrigin = BitmapFrame.Create(ms,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        }
                    }
                }
            }
        }
        
        public Photo(ProductImageViewModel path) // конструктор получения изображений из базы
        {
            
            _path = path.GetImageSmall;
            _pathOriginal = path.GetImageOriginal;
            
            using (var image = System.Drawing.Image.FromFile(_pathOriginal))
            {
                using (var newImageSmall = ImageWorker.ConverImageToBitmap(image, 130, 130))
                {
                    if (newImageSmall != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            newImageSmall.Save(ms, ImageFormat.Bmp);
                            _image = BitmapFrame.Create(ms,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        }
                    }
                }
                using (var newImageOrigin = ImageWorker.ConverImageToBitmap(image, image.Width, image.Height))
                {
                    if (newImageOrigin != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            newImageOrigin.Save(ms, ImageFormat.Bmp);
                            _imageOrigin = BitmapFrame.Create(ms,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.OnLoad);
                        }
                    }
                }
            }


        }
        
        public string Source { get { return _path; } } // путь на уменьшенную фотку

        public string SourceOriginal { get { return _pathOriginal; } } // путь на оригинальную фотку

        public BitmapFrame ImageFrame { get { return _image; } set { _image = value; } }
        public BitmapFrame ImageFrameOrigin { get { return _imageOrigin; } set { _imageOrigin = value; } }

        public string ImageName { get { return Path.GetFileNameWithoutExtension(_pathOriginal); } }


    }
}
