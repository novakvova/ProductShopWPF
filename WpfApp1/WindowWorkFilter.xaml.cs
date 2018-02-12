using BLL.Abstract;
using BLL.Model;
using BLL.Provider;
using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WindowWorkFilter.xaml
    /// </summary>
    /// 


    public partial class WindowWorkFilter : Window
    {
        private readonly IFilterProvider _filterProvider;
        //List<ParentViewItem> parentItems = new List<ParentViewItem>();
        public WindowWorkFilter()
        {
            InitializeComponent();
            _filterProvider = new FilterProvider();

        }

        private void btnAddFilterName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxAddFilterName.Text;
            var viewItem = _filterProvider.AddFilterName(name);
            if (viewItem != null)
            {
                TreeViewItem newChild = new TreeViewItem();
                newChild.Header = viewItem;
                treeView1.Items.Add(newChild);
            }
            else
            {
                MessageBox.Show($"Назва фільтру вже існує");
            }
            //using (EfContext context = new EfContext())
            //{
            //    //var findFilter = context.FilterNames.SingleOrDefault(f => f.Name == fname);
            //    var findFilter = _FilterNameRepository.GetByName(fname);
            //    if (findFilter == null)
            //    {
            //        //MessageBox.Show($"Назва фільтра вже існує {findFilter.Id}");
            //        FilterName filterName = new FilterName
            //        {
            //            Name = fname
            //        };
            //        //context.FilterNames.Add(filterName);
            //        //context.SaveChanges();
            //        _FilterNameRepository.Add(filterName);
            //        _FilterNameRepository.SaveChanges();
            //        MyTreeViewItem viewItem = new MyTreeViewItem
            //        {
            //            Id = filterName.Id.ToString(),
            //            Name = filterName.Name
            //        };
            //        TreeViewItem parent = new TreeViewItem
            //        {
            //            Header = viewItem
            //        };
            //        treeView1.Items.Add(parent);
            //    }
            //    else
            //        MessageBox.Show($"Назва фільтра вже існує {findFilter.Id}");
            //}
        }

        private void btnFilterValue_Click(object sender, RoutedEventArgs e)
        {
             string name = txtFilterValue.Text;
            TreeViewItem newChild = new TreeViewItem();
            newChild.Header = name;
            if (treeView1.SelectedItem == null)
                return;
            var index = treeView1.Items.IndexOf(treeView1.SelectedItem);
            if (index == -1)
                return;
            var item = (TreeViewItem)treeView1.Items[index];//[0];
            if (item.Header is MyTreeViewItem)
            {
                _filterProvider.AddFilterValue(name, item.Header as MyTreeViewItem);
                refreshTreeView();
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //using (EfContext context = new EfContext())
            //{
            //    foreach (var item in parentItems)
            //    {
            //        if (item.Children.Count > 0)
            //        {
            //            using (TransactionScope scope = new TransactionScope())
            //            {
            //                FilterName filterName = new FilterName
            //                {
            //                    Name = item.Name
            //                };
            //                context.FilterNames.Add(filterName);
            //                context.SaveChanges();
            //                foreach (var child in item.Children)
            //                {
            //                    FilterValue filterValue = new FilterValue
            //                    {
            //                        Name = child
            //                    };
            //                    context.FilterValues.Add(filterValue);
            //                    context.SaveChanges();
            //                    FilterNameGroups fNameGroup = new FilterNameGroups
            //                    {
            //                        FilterNameId = filterName.Id,
            //                        FilterValueId = filterValue.Id
            //                    };
            //                    context.FilterGroups.Add(fNameGroup);
            //                    context.SaveChanges();
            //                }
            //                scope.Complete();
            //                //parentItems.Clear();
            //            }
            //        }
            //    }
            //}
            //refreshTreeView();


            //using (EfContext context = new EfContext())
            //{
            //    string fname = txtBoxAddFilterName.Text;
            //    var findFilter = context.FilterNames.SingleOrDefault(f => f.Name == fname);
            //    if (findFilter == null)
            //    {
            //        FilterName filterName = new FilterName
            //        {
            //            Name = fname
            //        };
            //        context.FilterNames.Add(filterName);
            //        context.SaveChanges();
            //    }
            //}
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TreeViewItem item = treeView1.SelectedItem as TreeViewItem;
            //if (item != null)
            //{
            //    ItemsControl parent = GetSelectedTreeViewItemParent(item);

            //    TreeViewItem treeitem = parent as TreeViewItem;
            //    try
            //    {
            //        string MyValue = treeitem.Header.ToString();//Gets you the immediate parent
            //        MessageBox.Show(MyValue);
            //    }
            //    catch { }
            //}
        }

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshTreeView();
        }


        private void refreshTreeView()
        {
            treeView1.Items.Clear();
            using (EfContext context = new EfContext())
            {
                var query = (from f in context.VFilterNameGroups.AsQueryable()
                             select new
                             {
                                 FNameId = f.FilterNameId,
                                 FName = f.FilterName,
                                 FValueId = f.FilterValueId,
                                 FValue = f.FilterValue
                             });

                var groupNames = (from f in query
                                  group f by new
                                  {
                                      Id = f.FNameId,
                                      Name = f.FName
                                  } into g
                                  orderby g.Key.Name
                                  select g);



                foreach (var filterName in groupNames)
                {
                    var FName = new MyTreeViewItem
                    {
                        Id = filterName.Key.Id.ToString(),
                        Name = filterName.Key.Name
                    };
                    TreeViewItem parent = new TreeViewItem();

                    parent.Header = FName;
                    treeView1.Items.Add(parent);

                    var FValues = from v in filterName
                                  group v by new MyTreeViewItem
                                  {
                                      Id = v.FValueId.ToString(),
                                      Name = v.FValue
                                  };
                    foreach (var filterValue in FValues)
                    {
                        if (string.IsNullOrEmpty(filterValue.Key.Name))
                            continue;
                        TreeViewItem newChild = new TreeViewItem();
                        newChild.Header = filterValue.Key;
                        parent.Items.Add(newChild);
                    }
                }
            }
        }

        private void btnFilterDelete_Click(object sender, RoutedEventArgs e)
        {
            //using (EfContext context = new EfContext())
            //{
            //    using (TransactionScope scope = new TransactionScope())
            //    {
            //        TreeViewItem item = treeView1.SelectedItem as TreeViewItem;
            //        if (item != null)
            //        {
            //            ItemsControl parent = GetSelectedTreeViewItemParent(item);
            //            TreeViewItem treeItem = parent as TreeViewItem;

            //            if (treeItem != null )
            //            {

            //                MyTreeViewItem myValue = item.Header as MyTreeViewItem;
            //                MyTreeViewItem parentItem = treeItem.Header as MyTreeViewItem;
            //                var delValue = context.FilterValues.SingleOrDefault(f => f.Id.ToString() == myValue.Id);
            //                var delGroup = context.VFilterNameGroups
            //                    .SingleOrDefault(f => f.FilterNameId.ToString() == parentItem.Id
            //                                            && f.FilterValueId.ToString() == myValue.Id);
            //                if (delValue != null)
            //                {
            //                    context.VFilterNameGroups.Remove(delGroup);
            //                    context.FilterValues.Remove(delValue);
            //                    context.SaveChanges();
            //                }
            //                refreshTreeView();
            //            }
            //            else
            //            {
            //                MyTreeViewItem myName = treeView1.SelectedItem as MyTreeViewItem;
            //                var delName = context.FilterNames.SingleOrDefault(f => f.Name == myName.Name);
            //                context.FilterNames.Remove(delName);
            //            }
            //        }
            //        scope.Complete();
            //    }
            //}
            //refreshTreeView();

            TreeViewItem item = treeView1.SelectedItem as TreeViewItem;
            if (item != null)
            {
                ItemsControl parent = GetSelectedTreeViewItemParent(item);
                TreeViewItem treeitem = parent as TreeViewItem;
                if (treeitem != null)
                {
                    MyTreeViewItem myValue = item.Header as MyTreeViewItem;
                    MyTreeViewItem parentItem = treeitem.Header as MyTreeViewItem;

                    _filterProvider.RemoveFilterValue(int.Parse(myValue.Id), int.Parse(parentItem.Id));
                }
                else
                {
                    _filterProvider.RemoveFilterName(item);
                }
                refreshTreeView();
            }
        }
    }
}
