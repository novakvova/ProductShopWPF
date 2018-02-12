using BLL.Abstract;
using BLL.Model;
using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Concrete;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1;

namespace BLL.Provider
{
    public class ProductProvider : IProductProvider
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;

       public ProductProvider()
        {
            EfContext context = new EfContext();
            _productRepository = new ProductRepository(context);
            _productImageRepository = new ProductImageRepository(context);
        }
        public Product AddProduct(ProductAddViewModel productAdd)
        {
            //MessageBox.Show(Environment.CurrentDirectory);
            Product product = new Product
            {
                Name = productAdd.Name,
                CategoryId = productAdd.CategoryId,
                Price = productAdd.Price,
                Quantity = productAdd.Quantity,
                DateCreate = DateTime.Now
            };
            _productRepository.Add(product);
            _productRepository.SaveChanges();
            if (productAdd.Images.Count > 0)
            {
                List<string> imgTemp = new List<string>();
                foreach (var p in productAdd.Images)
                {
                    //MessageBox.Show(p.SourceOriginal);
                    string fOriginalName = Path.GetFileName(p.SourceOriginal);
                    string fOriginalPath = Path.GetDirectoryName(p.SourceOriginal);
                    string fSmallName = Path.GetFileName(p.Source);
                    string fSmallPath = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImagePath"].ToString();
                    string fImages = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImageStore"].ToString();
                    // Will not overwrite if the destination file already exists.
                    try
                    {
                        File.Copy(Path.Combine(fOriginalPath, fOriginalName), Path.Combine(fImages, "o_" + fSmallName));
                        imgTemp.Add("o_" + fSmallName);
                        File.Copy(Path.Combine(fSmallPath, fSmallName), Path.Combine(fImages, "s_" + fSmallName));
                        imgTemp.Add("s_" + fSmallName);

                        ProductImage image = new ProductImage // сохраняем в таблицу ProductImages  
                        {
                            Name = fSmallName, // сохраняем имя без приставки
                            ProductId = product.Id
                        };
                        _productImageRepository.Add(image);
                        // Catch exception if the file was already copied.
                    }
                    catch (IOException copyError)
                    {
                        MessageBox.Show(copyError.Message);
                        foreach (var item in imgTemp)
                        {
                            File.Delete(Path.Combine(fImages, item));
                        }
                        return null;
                    }
                }
               _productImageRepository.SaveChanges();
            }
            return product;
        }

        public IQueryable<Product> GetAllProducts()
        {
            IQueryable<Product> allProducts = _productRepository.GetAll();
            //foreach (Product product in allProducts)
            //{
            //    foreach (ProductImage item in _productImageRepository.GetAll(product.Id))
            //    {
            //        product.ProductImages.Add(item);
            //    }
            //}
            return allProducts;
        }

        public IQueryable<Product> GetAllProductsByName(string name)
        {
            var allProducts = _productRepository.GetAll().Where(n => n.Name.StartsWith(name));
            //foreach (var product in allProducts)
            //{
            //    foreach (var item in _productImageRepository.GetAll(product.Id))
            //    {
            //        product.ProductImages.Add(item);
            //    }
            //}
            return allProducts;
        }
        public void RemoveProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
