using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BLL.Abstract
{
    public interface IFilterProvider
    {
        MyTreeViewItem AddFilterName(string name);
        MyTreeViewItem AddFilterValue(string name, MyTreeViewItem item);

        void RemoveFilterName(TreeViewItem deleteItem);
        void RemoveFilterValue(int itemId, int parentitemId);
    }
}
