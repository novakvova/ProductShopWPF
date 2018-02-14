using BLL.Abstract;
using BLL.Model;
using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Concrete;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApp1;
using System.Data.Entity;

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
                    string fSaveName = Guid.NewGuid().ToString() + ".jpg"; //Path.GetFileName(p.Source);
                    string fImages = Environment.CurrentDirectory + ConfigurationManager.AppSettings["ImageStore"].ToString();
                    string fSaveImageBig = "o_" + fSaveName;
                    string fSaveImageSmall = "s_" + fSaveName;
                    // Will not overwrite if the destination file already exists.
                    try
                    {
                        File.Copy(p.SourceOriginal, Path.Combine(fImages, fSaveImageBig));
                        imgTemp.Add(fSaveImageBig);
                        using (FileStream stream = 
                            new FileStream(Path.Combine(fImages, fSaveImageSmall),
                            FileMode.Create))
                        {
                            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                            encoder.Frames.Add(p.ImageFrame);
                            encoder.Save(stream);
                            imgTemp.Add(fSaveImageSmall);
                        }
                        
                        ProductImage image = new ProductImage // сохраняем в таблицу ProductImages  
                        {
                            Name = fSaveName, // сохраняем имя без приставки
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
        private Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              PixelFormat.Format32bppPArgb);
            BitmapData data = bmp.LockBits(
              new Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              PixelFormat.Format32bppPArgb);
            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
        public IQueryable<Product> GetAllProducts()
        {
            IQueryable<Product> allProducts = _productRepository.GetAll();
            
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

        public IList<ProductItemViewModel> FindProducts(string name)
        {
            var model = _productRepository
                .GetAll()
                .Include(c=>c.Categories)
                .Include(i=>i.ProductImages)
                .Where(p=>p.Name.StartsWith(name))
                .Select(p => new ProductItemViewModel
                {
                    Id=p.Id,
                    Name=p.Name,
                    Category=p.Categories.Name,
                    Price=p.Price,
                    DateCreate=p.DateCreate,
                    Quantity=p.Quantity,
                    ProductImages=p.ProductImages
                    .Select(i=>new ProductImageViewModel
                    {
                        Id=i.Id,
                        Name=i.Name
                    }).ToList()
                }).ToList();
            return model;
        }
    }
}
