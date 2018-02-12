using BLL.Abstract;
using BLL.Provider;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {
        private readonly IProductProvider _productProvider;
        public BLL.Model.PhotoCollection Photos;
        public MainWindow()
        {
            InitializeComponent();
            _productProvider = new ProductProvider();
            _productProvider.GetAllProducts();
            Photos = (BLL.Model.PhotoCollection)(this.Resources["Photos"] as ObjectDataProvider).Data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd wnd = new WindowAdd();
            wnd.Show();
        }

        private void btnAddShowFilter_Click(object sender, RoutedEventArgs e)
        {
            WindowWorkFilter dlg = new WindowWorkFilter();
            dlg.ShowDialog();
        }

        private void buttonAC_Click(object sender, RoutedEventArgs e)
        {
            WindowAutoComplete dlg = new WindowAutoComplete();
            dlg.ShowDialog();
        }

        private void tlACPeople_Populating(object sender, PopulatingEventArgs e)
        {
            string text = tlACPeople.Text;
            var result = _productProvider.GetAllProductsByName(text).ToList();
            tlACPeople.ItemsSource = result;
            tlACPeople.PopulateComplete();

        }
        private void tlACPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tlACPeople.SelectedItem != null)
            {
                var product = tlACPeople.SelectedItem as Product;
                lblProductName.Content = product.Name;
                lblProductPrice.Content = product.Price;
                lblProductQty.Content = product.Quantity;
                if (Photos.Count > 0)
                {
                    Photos.Clear();
                }
                Photos.AddProductImages(product.ProductImages);
                ImageOfProduct.Source = new BitmapImage(new Uri(product.FirstImageOriginal));
                //txtBox1.Text = (tlACPeople.SelectedItem as Person).Id.ToString();
                //imgBox1.Source = new BitmapImage(new Uri((tlACPeople.SelectedItem as Person).PatchPicture.ToString()));
            }
        }

        private void PhotosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
