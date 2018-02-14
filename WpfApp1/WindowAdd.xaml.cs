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

            this.Close();
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

