using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class TransactionStore
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly ProductRepository _productRepository;

        public TransactionStore(TransactionRepository transactionRepository, ProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }
        public void DisplaySubMenu()
        {
            while (true)
            {
                Console.WriteLine("======Transaction Management======");
                Console.WriteLine("1. Add Stock");
                Console.WriteLine("2. Remove Stock");
                Console.WriteLine("3. View Transaction History");
                Console.WriteLine("4. Go Back To Main Menu");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddStock();
                            break;
                        case "2":
                            RemoveStock();
                            break;
                        case "3":
                            ViewTransactionHistory();
                            break;
                        case "4":
                            return;
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 4 only...");
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

        public void AddStock()
        {
            try
            {
                Console.WriteLine("Enter Product ID to add stock:");
                int productId = Convert.ToInt32(Console.ReadLine());

                var product = _productRepository.GetById(productId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product not found.");
                }

                Console.WriteLine("Enter Quantity to Add:");
                int quantityToAdd = Convert.ToInt32(Console.ReadLine());

                product.Quantity += quantityToAdd;
                _productRepository.Update(product);

                var transaction = new Transaction
                {
                    ProductId = productId,
                    Type = "Add",
                    Quantity = quantityToAdd,
                    Date = DateTime.Now
                };

                _transactionRepository.Add(transaction);
                Console.WriteLine("Stock added successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RemoveStock()
        {
            try
            {
                Console.WriteLine("Enter Product ID to remove stock:");
                int productId = Convert.ToInt32(Console.ReadLine());

                var product = _productRepository.GetById(productId);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product not found.");
                }

                Console.WriteLine("Enter Quantity to Remove:");
                int quantityToRemove = Convert.ToInt32(Console.ReadLine());

                if (product.Quantity < quantityToRemove)
                {
                    Console.WriteLine("Insufficient stock.");
                    return;
                }

                product.Quantity -= quantityToRemove;
                _productRepository.Update(product);

                var transaction = new Transaction
                {
                    ProductId = productId,
                    Type = "Remove",
                    Quantity = quantityToRemove,
                    Date = DateTime.Now
                };

                _transactionRepository.Add(transaction);
                Console.WriteLine("Stock removed successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewTransactionHistory()
        {
            var transactions = _transactionRepository.GetAll();
            if (transactions.Count == 0)
            {
                throw new TransactionNotFoundException("No transactions found.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Transaction ID: {transaction.TransactionId}, Product ID: {transaction.ProductId}, Type: {transaction.Type}, Quantity: {transaction.Quantity}, Date: {transaction.Date}");
                }
            }
        }
    }
}
