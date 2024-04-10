namespace FoodStore.Core.Exceptions.Products
{
    public class InvalidProductIDException : ArgumentException
    {
        public InvalidProductIDException() : base()
        {
        }

        public InvalidProductIDException(string? message) : base(message)
        {
        }

        public InvalidProductIDException(string? message, Exception? innerException)
        {
        }
    }
}
