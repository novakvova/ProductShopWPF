using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Abstract
{
    public interface IProductImageRepository
    {
        ProductImage Add(ProductImage prodImage);

        void Remove(ProductImage prodImage);

        ProductImage GetPriorityImage(int ProductId);
        IQueryable<ProductImage> GetAll(int ProductId);
        int SaveChanges();
    }
}
