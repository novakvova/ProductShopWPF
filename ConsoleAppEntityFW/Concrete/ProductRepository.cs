using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ProductRepository : IProductRepository
    {
        private readonly EfContext _context;

        public ProductRepository(EfContext db)
        {
            _context = db;
        }
        public Product Add(Product Name)
        {
            _context.Products.Add(Name);
            return Name;
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.AsQueryable();
        }
        

        public Product GetByName(string name)
        {
            return this.GetAll().SingleOrDefault(p => p.Name == name);
        }

        public void Remove(Product Name)
        {
            _context.Products.Remove(Name);
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }
    }
}
