using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Abstract
{
    public interface IFilterNameRepository
    {
        FilterName Add(FilterName filterName);

        void Remove(FilterName filterName);

        FilterName GetByName(string name);
        IQueryable<FilterName> GetAll();
        int SaveChanges();

    }
}
