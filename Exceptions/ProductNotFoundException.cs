namespace InventoryManagement.Exceptions
{
    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
}
