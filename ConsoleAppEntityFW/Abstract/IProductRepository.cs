using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Abstract
{
    public interface IProductRepository
    {
        Product Add(Product Name);

        void Remove(Product Name);

        Product GetByName(string name);
        IQueryable<Product> GetAll();


        int SaveChanges();
    }
}
