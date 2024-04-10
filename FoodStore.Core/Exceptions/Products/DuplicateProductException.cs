namespace FoodStore.Core.Exceptions.Products
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException() : base()
        {
        }

        public DuplicateProductException(string? message) : base(message)
        {
        }

        public DuplicateProductException(string? message, Exception? innerException)
        {
        }
    }
}
