using BLL.Abstract;
using BLL.Model;
using BLL.Provider;
using ConsoleAppEntityFW.Entitys;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        private readonly IProductProvider _productProvider;
        //public static ObservableCollection<Category> cat;
        //public static ObservableCollection<Photo> Photos = new ObservableCollection<Photo>();
        public BLL.Model.PhotoCollection Photos;
        public WindowAdd()
        {

            InitializeComponent();
            _productProvider = new ProductProvider();
            //var z = (PhotoCollection)(this.Resources["Photos"] as ObjectDataProvider).Data;
            Photos = (BLL.Model.PhotoCollection)(this.Resources["Photos"] as ObjectDataProvider).Data;
            //is.Photos. = //Environment.CurrentDirectory + "\\Images";
            using (EfContext context = new EfContext())
            {
                //cat  = context.Categories.ToList();
                //treeView1.ItemsSource = (from c in context.Categories
                //                select new { c.Id, c.Name}).ToList();

                //foreach (var item in context.Categories)
                //{
                //    ComboCategory.Items.Add(item.Name);
                //}
                ComboCategory.ItemsSource = (from c in context.Categories
                                             select c).ToList();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductAddViewModel product = new ProductAddViewModel()
            {
                Name = TxtBoxName.Text,
                CategoryId = (ComboCategory.SelectedItem as Category).Id,
                Price = float.Parse(TxtBoxPrice.Text),
                Quantity = int.Parse(TxtBoxQty.Text),
                Images = Photos
            };
            var prod = _productProvider.AddProduct(product);
            if(prod != null)
                MessageBox.Show($"Продукт успешно сохранен. ID = {prod.Id.ToString()}");

            #region OldAddCode
            //foreach (var p in Photos)
            //{
            //    MessageBox.Show(p.SourceOriginal);
            //}

            //using (EfContext context = new EfContext())
            //{
            //Product product = new Product
            //{
            //    Name = TxtBoxName.Text,
            //    CategoryId = (ComboCategory.SelectedItem as Category).Id,
            //    Price = float.Parse(TxtBoxPrice.Text),
            //    Quantity = int.Parse(TxtBoxQty.Text),
            //    DateCreate = DateTime.Now,
            //};
            //context.Products.Add(product);
            //context.SaveChanges();
            //MessageBox.Show($"Id = {product.Id.ToString()}");
            //if (Photos.Count > 0)
            //{

            //    List<string> imgTemp = new List<string>();
            //    foreach (var p in Photos)
            //    {
            //        //MessageBox.Show(p.SourceOriginal);
            //        string fOriginalName = Path.GetFileName(p.SourceOriginal);
            //        string fOriginalPath = Path.GetDirectoryName(p.SourceOriginal);
            //        string fSmallName = Path.GetFileName(p.Source);
            //        string fSmallPath = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString();
            //        string fImages = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImageStore"].ToString();
            //        // Will not overwrite if the destination file already exists.
            //        try
            //        {
            //            File.Copy(Path.Combine(fOriginalPath, fOriginalName), Path.Combine(fImages, "o_" + fSmallName));
            //            imgTemp.Add("o_" + fSmallName);
            //            File.Copy(Path.Combine(fSmallPath, fSmallName), Path.Combine(fImages, "s_" + fSmallName));
            //            imgTemp.Add("s_" + fSmallName);

            //            ProductImage image = new ProductImage // сохраняем в таблицу ProductImages  
            //            {
            //                Name = fSmallName,
            //                ProductId = product.Id
            //            };
            //            context.ProductImages.Add(image);

            //            // Catch exception if the file was already copied.
            //        }
            //        catch (IOException copyError)
            //        {
            //            MessageBox.Show(copyError.Message);
            //            foreach (var item in imgTemp)
            //            {
            //                File.Delete(Path.Combine(fImages, item));
            //            }
            //            return;
            //        }

            //    }
            //    context.SaveChanges();
            //}
            //}
            #endregion
        }

        private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames) // мульти добавление изображений
                {
                    this.Photos.AddImage(filename);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //Photos.Clear();
            //Directory.Delete(Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString(), true); //true - если директория не пуста (удалит и файлы и папки)
        }
    }
}

