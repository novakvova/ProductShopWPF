using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Concrete
{
    public interface IFilterValueRepository
    {
        FilterValue Add(FilterValue filterValue);

        void Remove(FilterValue filterValue);

        FilterValue GetByValue(string value);
        IQueryable<FilterValue> GetAll();
        int SaveChanges();
    }
}
