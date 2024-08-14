using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class ProductStore
    {
        private readonly ProductRepository _productRepository;

        public ProductStore(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void DisplaySubMenu()
        {
            while (true)
            {
                Console.WriteLine("======Product Management======");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. View Product's Details");
                Console.WriteLine("5. View All Products");
                Console.WriteLine("6. Go Back To Main Menu");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddProduct();
                            break;
                        case "2":
                            UpdateProduct();
                            break;
                        case "3":
                            DeleteProduct();
                            break;
                        case "4":
                            ViewProductDetails();
                            break;
                        case "5":
                            ViewAllProducts();
                            break;
                        case "6":
                            return;
                        default:
                            throw new InvalidChoiceException("Invalid Choice, Please Choose Between 1 and 6 only...");
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

        public void AddProduct()
        {
            try
            {
                Console.WriteLine("Enter Product Name: ");
                string name = Console.ReadLine();

                var newProduct = _productRepository.GetByName(name);
                if (newProduct != null)
                {
                    throw new ProductAlreadyExistsException("Product with same name already exists.");
                }

                Console.WriteLine("Enter Product Description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter Product Quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                if (quantity <= 0)
                {
                    throw new InvalidValueException("Quantity must be greater than zero.");
                }

                Console.WriteLine("Enter Product Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                if (price <= 0)
                {
                    throw new InvalidValueException("Price must be a positive number.");
                }

                var product = new Product
                {
                    Name = name,
                    Description = description,
                    Quantity = quantity,
                    Price = price
                };
                _productRepository.Add(product);

                Console.WriteLine("Product added successfully.");
            }
            catch (ProductAlreadyExistsException ex)
            {
                Console.WriteLine("Product with same name already exists.");
            }
            catch (InvalidValueException ive)
            {
                Console.WriteLine(ive.Message);
            }
        }

        public void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Enter Product Id to Update: ");
                int id = Convert.ToInt32(Console.ReadLine());


                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product Not Found...");
                }

                Console.WriteLine("Enter New Product Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter New Product Description: ");
                string description = Console.ReadLine();

                Console.WriteLine("Enter New Product Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                if (price <= 0)
                {
                    throw new InvalidValueException("Price must be a positive number.");
                }

                product.Name = name;
                product.Description = description;
                product.Price = price;

                _productRepository.Update(product);
                Console.WriteLine("Product Updated Successfully...");
            }
            catch (ProductNotFoundException pfe)
            {
                Console.WriteLine(pfe.Message);
            }

        }

        public void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter Product ID to delete:");
                int id = Convert.ToInt32(Console.ReadLine());

                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    throw new ProductNotFoundException("Product Not Found...");
                }

                _productRepository.Delete(product);

                Console.WriteLine("Product deleted successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewProductDetails()
        {
            try
            {
                Console.WriteLine("Enter Product ID to view details:");
                int id = Convert.ToInt32(Console.ReadLine());

                var product = _productRepository.GetById(id);
                if (product != null)
                {
                    Console.WriteLine(product);
                }
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void ViewAllProducts()
        {
            var products = _productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

    }

}

