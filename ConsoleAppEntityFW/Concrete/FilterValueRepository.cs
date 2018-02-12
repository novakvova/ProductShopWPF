using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppEntityFW.Entitys;

namespace ConsoleAppEntityFW.Concrete
{
    public class FilterValueRepository : IFilterValueRepository
    {
        private readonly EfContext _context;

        public FilterValueRepository(EfContext db)
        {
            _context = db;
        }
        public FilterValue Add(FilterValue filterValue)
        {
            _context.FilterValues.Add(filterValue);
            return filterValue;
        }

        public void Remove(FilterValue filterValue)
        {
            _context.FilterValues.Remove(filterValue);
        }

        public IQueryable<FilterValue> GetAll()
        {
            return _context.FilterValues.AsQueryable();
        }

        public FilterValue GetByValue(string value)
        {
            return this.GetAll().SingleOrDefault(f => f.Name == value);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
