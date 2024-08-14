namespace InventoryManagement.Exceptions
{
    internal class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException(string message) : base(message) { }
    }
}
