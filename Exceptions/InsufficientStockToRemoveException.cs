namespace InventoryManagement.Exceptions
{
    internal class InsufficientStockToRemoveException : Exception
    {
        public InsufficientStockToRemoveException(string message) : base(message) { }

    }
}
