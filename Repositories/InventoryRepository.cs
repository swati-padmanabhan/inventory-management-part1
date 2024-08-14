using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repositories
{
    internal class InventoryRepository
    {
        private readonly InventoryContext _context;

        public InventoryRepository(InventoryContext context)
        {
            _context = context;
        }

        public List<Inventory> GetAll()
        {
            return _context.Inventories
                .Include(inventory => inventory.Products)
                .Include(inventory => inventory.Suppliers)
                .Include(inventory => inventory.Transactions)
                .ToList();
        }
    }
}
