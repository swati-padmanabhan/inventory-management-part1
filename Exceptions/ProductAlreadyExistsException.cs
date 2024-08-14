namespace InventoryManagement.Exceptions
{
    internal class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string message) : base(message) { }
    }
}
