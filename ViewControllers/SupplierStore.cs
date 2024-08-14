using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;

namespace InventoryManagement.ViewControllers
{
    internal class SupplierStore
    {
        private readonly SupplierRepository _supplierRepository;

        public SupplierStore(SupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public void DisplaySubMenu()
        {
            while (true)
            {
                Console.WriteLine("======Supplier Management======");
                Console.WriteLine("1. Add Supplier");
                Console.WriteLine("2. Update Supplier");
                Console.WriteLine("3. Delete Supplier");
                Console.WriteLine("4. View Supplier's Details");
                Console.WriteLine("5. View All Suppliers");
                Console.WriteLine("6. Go Back To Main Menu");

                try
                {
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddSupplier();
                            break;
                        case "2":
                            UpdateSupplier();
                            break;
                        case "3":
                            DeleteSupplier();
                            break;
                        case "4":
                            ViewSupplierDetails();
                            break;
                        case "5":
                            ViewAllSuppliers();
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

        public void AddSupplier()
        {
            try
            {
                Console.Write("Enter Supplier Name: ");
                string name = Console.ReadLine();


                var newSupplier = _supplierRepository.GetByName(name);
                if (newSupplier != null)
                {
                    throw new SupplierAlreadyExistsException("Supplier with same name already exists.");
                }

                Console.Write("Enter Supplier Contact Information: ");
                string contactInformation = Console.ReadLine();

                var supplier = new Supplier
                {
                    Name = name,
                    ContactInformation = contactInformation
                };

                _supplierRepository.Add(supplier);
                Console.WriteLine("Supplier added successfully.");
            }
            catch (SupplierAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateSupplier()
        {
            try
            {
                Console.WriteLine("Enter Suppplier Id to Update: ");
                int id = Convert.ToInt32(Console.ReadLine());


                var supplier = _supplierRepository.GetById(id);
                if (supplier == null)
                {
                    throw new ProductNotFoundException("Supplier Not Found...");
                }

                Console.WriteLine("Enter New Supplier Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter New Product Contact Information: ");
                string contactInformation = Console.ReadLine();


                supplier.Name = name;
                supplier.ContactInformation = contactInformation;

                _supplierRepository.Update(supplier);
                Console.WriteLine("Supplier Updated Successfully...");
            }
            catch (SupplierNotFoundException sfe)
            {
                Console.WriteLine(sfe.Message);
            }
        }

        public void DeleteSupplier()
        {
            try
            {
                Console.Write("Enter Supplier ID to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());

                var supplier = _supplierRepository.GetById(id);
                if (supplier == null)
                {
                    throw new SupplierNotFoundException("Supplier Not Found...");
                }

                _supplierRepository.Delete(supplier);
                Console.WriteLine("Supplier deleted successfully.");
            }
            catch (SupplierNotFoundException sfe)
            {
                Console.WriteLine(sfe.Message);
            }
        }

        public void ViewSupplierDetails()
        {
            try
            {
                Console.Write("Enter Supplier ID to view details: ");
                int id = Convert.ToInt32(Console.ReadLine());

                var supplier = _supplierRepository.GetById(id);
                if (supplier != null)
                {
                    Console.WriteLine(supplier);
                }

                else
                {
                    throw new SupplierNotFoundException("Supplier not found.");
                }
            }
            catch (SupplierNotFoundException sfe)
            {
                Console.WriteLine(sfe.Message);
            }

        }


        public void ViewAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAll();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine(supplier);
            }
        }
    }
}
