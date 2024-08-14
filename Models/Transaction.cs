using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    internal class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; } //FK
        public string Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Inventory")]
        public int? InventoryId { get; set; }

        public Transaction() { }

        public Transaction(int transactionId, int productId, string type, int quantity, DateTime date)
        {
            TransactionId = transactionId;
            ProductId = productId;
            Type = type;
            Quantity = quantity;
            Date = date;
        }

        public override string ToString()
        {
            return $"Transaction Id: {TransactionId}\n" +
                $"Inventory Id: {InventoryId}\n" +
                $"Product Id: {ProductId}\n" +
                $"Transaction Type: {Type}\n" +
                $"Quantity: {Quantity}\n" +
                $"Date: {Date}\n";
        }

    }
}
