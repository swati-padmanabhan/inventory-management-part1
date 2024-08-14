using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    internal class ProductRepository
    {
        private readonly InventoryContext _context;

        public ProductRepository(InventoryContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            var product = _context.Products.Where(x => x.ProductId == id).FirstOrDefault();
            return product;
        }
        public Product GetByName(string name)
        {
            var product = _context.Products.Where(x => x.Name == name).FirstOrDefault();
            return product;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

    }
}
