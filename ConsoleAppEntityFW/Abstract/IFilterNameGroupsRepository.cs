using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Abstract
{
    public interface IFilterNameGroupsRepository
    {
        FilterNameGroups Add(FilterNameGroups filterGroups);

        void Remove(FilterNameGroups filterGroups);

        FilterNameGroups GetByNameGroups(string NameGroups);
        IQueryable<FilterNameGroups> GetAll();
        int SaveChanges();
    }
}
