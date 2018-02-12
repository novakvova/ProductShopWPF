using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Concrete
{
    public class FilterNameGroupsRepository : IFilterNameGroupsRepository
    {
        private readonly EfContext _context;

        public FilterNameGroupsRepository(EfContext db)
        {
            _context = db;
        }

        public Entitys.FilterNameGroups Add(Entitys.FilterNameGroups filterGroups)
        {
            _context.FilterGroups.Add(filterGroups);
            return filterGroups;
        }

        public void Remove(FilterNameGroups filterGroups)
        {
            _context.FilterGroups.Remove(filterGroups);
        }

        public IQueryable<Entitys.FilterNameGroups> GetAll()
        {
            return _context.FilterGroups.AsQueryable();
        }

        public Entitys.FilterNameGroups GetByNameGroups(string NameGroups)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }
    }
}
