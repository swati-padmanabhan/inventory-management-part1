using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    internal class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Inventory")]
        public int? InventoryId { get; set; }


        public Product() { }

        public Product(string name, string description, double price, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Product Id: {ProductId}\n" +
                $"Inventory Id : {InventoryId}\n" +
                $"Name: {Name}\n" +
                $"Description: {Description}\n" +
                $"Quantity: {Quantity}\n" +
                $"Price: {Price}\n";
        }

    }
}
