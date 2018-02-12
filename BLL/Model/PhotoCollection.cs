using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model
{
    public class PhotoCollection : ObservableCollection<Photo>
    {
        //private DirectoryInfo _directory;

        public PhotoCollection() { }
        //public PhotoCollection(string path) : this(new DirectoryInfo(path)) { }
        //public PhotoCollection(DirectoryInfo directory)
        //{
        //    _directory = directory;
        //    //Update();
        //}
        public void AddImage(string pathFile)
        {
            Add(new Photo(pathFile));
        }
        public void AddProductImages(ICollection<ProductImage> ProductImages)
        {
            foreach (var image in ProductImages)
            {
                Add(new Photo(image));
            }
        }
        //public string Path
        //{
        //    set
        //    {
        //        _directory = new DirectoryInfo(value);
        //        //Update();
        //    }
        //    get { return _directory.FullName; }
        //}

        //public DirectoryInfo Dir
        //{
        //    set
        //    {
        //        _directory = value;
        //        //Update();
        //    }
        //    get { return _directory; }
        //}

        //private void Update()
        //{
        //    this.Clear();
        //    try
        //    {
        //        //Directory.Delete(Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString(), true); //true - если директория не пуста (удалит и файлы и папки)
        //        //Directory.CreateDirectory(Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString());
        //    }
        //    catch { }
        //    //
        //    try
        //    {
        //        //foreach (FileInfo f in _directory.GetFiles("*.jpg"))
        //        //{
        //        //Add(new Photo(f.FullName));
        //        //}
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        System.Windows.MessageBox.Show("Текущая директория не найдена!");
        //    }
        //}
    }
}
