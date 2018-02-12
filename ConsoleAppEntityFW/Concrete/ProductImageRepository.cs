using ConsoleAppEntityFW.Abstract;
using ConsoleAppEntityFW.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Concrete
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly EfContext _context;

        public ProductImageRepository(EfContext db)
        {
            _context = db;
        }

        public ProductImage Add(ProductImage prodImage)
        {
            _context.ProductImages.Add(prodImage);
            return prodImage;
        }

        public IQueryable<ProductImage> GetAll(int ProductId)
        {
            //return _context.ProductImages.AsQueryable().Where(i => i.ProductId == ProductId);
            return _context.ProductImages.Where(i => i.ProductId == ProductId).AsQueryable();
        }

        public ProductImage GetPriorityImage(int ProductId)
        {
            return this.GetAll(ProductId).First(); // тимчасово вертає перше зображення ----> добавити поле в таблицю (byte Priority) тоды по ньому вертити прыоритетне зображення
        }

        public void Remove(ProductImage prodImage)
        {
            _context.ProductImages.Remove(prodImage);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
