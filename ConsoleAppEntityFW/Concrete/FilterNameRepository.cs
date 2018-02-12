using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Concrete
{
    public class FilterNameRepository : IFilterNameRepository
    {
        private readonly EfContext _context;
        
        public FilterNameRepository(EfContext db)
        {
            _context = db;
        }

        public FilterName Add(FilterName filterName)
        {
            _context.FilterNames.Add(filterName);
            return filterName; 
        }

        public void Remove(FilterName filterName)
        {
            _context.FilterNames.Remove(filterName);
        }
        public IQueryable<FilterName> GetAll()
        {
            return _context.FilterNames.AsQueryable();
        }

        public FilterName GetByName(string name)
        {
            return this.GetAll().SingleOrDefault(f => f.Name == name);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }
}
