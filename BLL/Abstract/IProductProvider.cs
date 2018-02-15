using BLL.Model;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1;

namespace BLL.Abstract
{
    public interface IProductProvider
    {
        Product AddProduct(ProductAddViewModel productAdd);

        IQueryable<Product> GetAllProducts();

        IQueryable<Product> GetAllProductsByName(string name);
        IList<ProductItemViewModel> FindProducts(string name, int getCount=2);
        int CountFindProducts(string name);
        void RemoveProduct(int productId);
    }
}
