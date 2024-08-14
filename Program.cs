using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Repositories;
using InventoryManagement.ViewControllers;

namespace InventoryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new InventoryContext();
            var productRepository = new ProductRepository(context);
            var supplierRepository = new SupplierRepository(context);
            var transactionRepository = new TransactionRepository(context);

            var productStore = new ProductStore(productRepository);
            var supplierStore = new SupplierStore(supplierRepository);
            var transactionStore = new TransactionStore(transactionRepository, productRepository);
            var reportStore = new ReportStore(productRepository, supplierRepository, transactionRepository);

            while (true)
            {
                Console.WriteLine("======Main Menu======");
                Console.WriteLine("1. Product Management");
                Console.WriteLine("2. Supplier Management");
                Console.WriteLine("3. Transaction Management");
                Console.WriteLine("4. Generate Report");
                Console.WriteLine("5. Exit");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            productStore.DisplaySubMenu();
                            break;
                        case "2":
                            supplierStore.DisplaySubMenu();
                            break;
                        case "3":
                            transactionStore.DisplaySubMenu();
                            break;
                        case "4":
                            reportStore.DisplaySubMenu();
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 5 only...");
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Input format is incorrect. Please enter valid data." + fe.Message);
                }
                catch (InvalidChoiceException ice)
                {
                    Console.WriteLine(ice.Message);
                }
            }
        }
    }
}
