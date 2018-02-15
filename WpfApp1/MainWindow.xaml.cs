using BLL.Abstract;
using BLL.Model;
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
            //_productProvider.GetAllProducts();
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
            var count = _productProvider.CountFindProducts(text);
            var result = _productProvider.FindProducts(text);

            tlACPeople.ItemsSource = result;
            tlACPeople.PopulateComplete();

        }
        private void tlACPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tlACPeople.SelectedItem != null)
            {
                var product = tlACPeople.SelectedItem as ProductItemViewModel;
                lblProductName.Content = product.Name;
                lblProductPrice.Content = product.Price;
                lblProductQty.Content = product.Quantity;
                if (Photos.Count > 0)
                {
                    Photos.Clear();
                }

                Photos.AddProductImages(product.ProductImages);
                ImageOfProduct.Source = product.FirstImageOriginal;//new Uri(product.FirstImageOriginal));

            }
        }

        private void PhotosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tlACPeople.SelectedItem != null)
            {
                if (PhotosListBox.SelectedIndex != -1)
                {
                    ImageOfProduct.Source = ((Photo)PhotosListBox.SelectedItem).ImageFrameOrigin;
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            //_productProvider.RemoveProduct(1008);
            //_productProvider.RemoveProduct(1004);
            if (tlACPeople.SelectedItem != null)
            {
                var product = tlACPeople.SelectedItem as ProductItemViewModel;
                MessageBoxResult rez =
                    MessageBox.Show("Ви дійсно бажаєте видалити продукт \"" + product.Name + "\" з бази?", "Видалення товару", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (rez == MessageBoxResult.Yes)
                {
                    _productProvider.RemoveProduct(product.Id);

                }
            }


        }
    }
}
