using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class ReportStore
    {
        private readonly ProductRepository _productRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly TransactionRepository _transactionRepository;

        public ReportStore(ProductRepository productRepository, SupplierRepository supplierRepository, TransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _transactionRepository = transactionRepository;
        }

        public void DisplaySubMenu()
        {
            while (true)
            {
                Console.WriteLine("======Generate Report======");
                Console.WriteLine("1. List of Products");
                Console.WriteLine("2. List of Suppliers");
                Console.WriteLine("3. List of Transactions");
                Console.WriteLine("4. Go Back To Main Menu");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            ListProducts();
                            break;
                        case "2":
                            ListSuppliers();
                            break;
                        case "3":
                            ListTransactions();
                            break;
                        case "4":
                            return;
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 5 only...");
                    }
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Input format is incorrect. Please enter valid data.");
                }
            }
        }


        public void ListProducts()
        {
            List<Product> products = _productRepository.GetAll();
            if (products.Count == 0)
            {
                throw new ProductNotFoundException("No Products found.");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}");
                }
            }
        }

        public void ListSuppliers()
        {
            List<Supplier> suppliers = _supplierRepository.GetAll();
            if (suppliers.Count == 0)
            {
                throw new SupplierNotFoundException("No Suppliers found.");
            }
            else
            {
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"Supplier ID: {supplier.SupplierId}, Name: {supplier.Name}");
                }
            }
        }

        public void ListTransactions()
        {
            List<Transaction> transactions = _transactionRepository.GetAll();
            if (transactions.Count == 0)
            {
                throw new TransactionNotFoundException("No Transactions found.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Transaction ID: {transaction.TransactionId}, Product ID: {transaction.ProductId}, Quantity: {transaction.Quantity}, Date: {transaction.Date}, Type: {transaction.Type}");
                }
            }
        }
    }
}
