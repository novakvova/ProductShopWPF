using BLL.Abstract;
using BLL.Model;
using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Concrete;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BLL.Provider
{
    public class FilterProvider : IFilterProvider
    {
        private readonly IFilterNameRepository _filterNameRepository;
        private readonly IFilterValueRepository _filterValueRepository;
        private readonly IFilterNameGroupsRepository _filterNameGroupsRepository;
        public FilterProvider()
        {
            EfContext context = new EfContext();
            _filterNameRepository = new FilterNameRepository(context);
            _filterValueRepository = new FilterValueRepository(context);
            _filterNameGroupsRepository = new FilterNameGroupsRepository(context);
        }
        public MyTreeViewItem AddFilterName(string name)
        {
            MyTreeViewItem item = null;
            var findFilter = _filterNameRepository.GetByName(name);
            if (findFilter == null)
            {
                FilterName filterName = new FilterName
                {
                    Name = name
                };
                _filterNameRepository.Add(filterName);
                _filterNameRepository.SaveChanges();
                item = new MyTreeViewItem
                {
                    Id = filterName.Id.ToString(),
                    Name = filterName.Name
                };
            }
            return item;
        }

        public MyTreeViewItem AddFilterValue(string name, MyTreeViewItem item)
        {
            var filterNameId = int.Parse(item.Id);
            FilterValue filterValue = new FilterValue
            {
                Name = name
            };
            _filterValueRepository.Add(filterValue);
            _filterValueRepository.SaveChanges();
            //context.FilterValues.Add(filterValue);
            //context.SaveChanges();
            FilterNameGroups filterNameGroup = new FilterNameGroups
            {
                FilterNameId = filterNameId,
                FilterValueId = filterValue.Id
            };
            _filterNameGroupsRepository.Add(filterNameGroup);
            _filterNameGroupsRepository.SaveChanges();
            //context.FilterGroups.Add(filterNameGroup);
            //context.SaveChanges();
            return item;
        }


        public void RemoveFilterName(TreeViewItem deleteItem)
        {
            if(deleteItem.Items.Count > 0)
            {
                foreach (TreeViewItem item in deleteItem.Items)
                {
                    RemoveFilterValue(int.Parse((item.Header as MyTreeViewItem).Id), int.Parse((deleteItem.Header as MyTreeViewItem).Id));
                }
            }
            MyTreeViewItem myDeleteItem = deleteItem.Header as MyTreeViewItem;
            var deleteName = _filterNameRepository.GetAll().SingleOrDefault(f => f.Id.ToString() == myDeleteItem.Id);
            _filterNameRepository.Remove(deleteName);
            _filterNameRepository.SaveChanges();
        }
        public void RemoveFilterValue(int itemId, int parentitemId)
        {
            var deleteValue = _filterValueRepository.GetAll()
                .SingleOrDefault(f => f.Id == itemId);
            var deleteGroup = _filterNameGroupsRepository.GetAll()
                .SingleOrDefault(f => f.FilterNameId == parentitemId
                && f.FilterValueId == deleteValue.Id);
            _filterValueRepository.Remove(deleteValue);
            _filterNameGroupsRepository.Remove(deleteGroup);
            _filterNameGroupsRepository.SaveChanges();
            _filterValueRepository.SaveChanges();
        }
       
    }
}
