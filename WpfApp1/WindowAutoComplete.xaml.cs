using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WindowAutoComplete.xaml
    /// </summary>
    /// 
    
    public interface IPeopleViewModel
    {
        IEnumerable<Person> People { get; }
        Person SelectedPerson { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        string patch;
        public string PatchPicture {
            get
            {
                return patch;
             }
            set
            {
                patch = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString() + value;
            }
        }
        public DateTime Birthday { get; set; }
        public string Name { get; set; }
    }
    public partial class WindowAutoComplete : Window
    {
        public List<Person> _list;
        public string path = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString();
        public WindowAutoComplete()
        {
            InitializeComponent();
            //Directory.CreateDirectory(Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString());
            _list = new List<Person>()
           {
               new Person
               {
                   Id = 1,
                   Name="asdfasdf",
                   PatchPicture = "1.png",
                   Birthday=DateTime.Now
               },
               new Person
               {
                   Id = 6,
                   Name="vvrv1",
                   PatchPicture = "2.png",
                   Birthday=DateTime.Now
               },
               new Person
               {
                   Id = 2,
                   Name="uuvvv",
                   PatchPicture = "3.png",
                   Birthday=DateTime.Now
               },
               new Person
               {
                   Id = 3,
                   Name="ttvvv",
                   PatchPicture = "4.png",
                   Birthday=DateTime.Now
               },
               new Person
               {
                   Id = 7,
                   Name="vvrv",
                   PatchPicture = "1.png",
                   Birthday=DateTime.Now
               },
               new Person
               {
                   Id = 4,
                   Name="rrvvv",
                   PatchPicture = "1.png",
                   Birthday=DateTime.Now
               },
               new Person
               {
                   Id = 5,
                   Name="vvrv",
                   Birthday=DateTime.Now
               }
           };
        }

        private void tlACPeople_Populating(object sender, PopulatingEventArgs e)
        {
            string text = tlACPeople.Text;
            var result = _list.Where(p => p.Name.Contains(text));
            tlACPeople.ItemsSource = result;
            tlACPeople.PopulateComplete();

        }

        private void tlACPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tlACPeople.SelectedItem != null)
            {
                txtBox1.Text = (tlACPeople.SelectedItem as Person).Id.ToString();

                imgBox1.Source = new BitmapImage(new Uri((tlACPeople.SelectedItem as Person).PatchPicture.ToString()));

            }
        }
    }
}
