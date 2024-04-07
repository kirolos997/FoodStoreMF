namespace FoodStore.Core.Exceptions.Categories
{
    public class InvalidCategoryIDException : ArgumentException
    {
        public InvalidCategoryIDException() : base()
        {
        }

        public InvalidCategoryIDException(string? message) : base(message)
        {
        }

        public InvalidCategoryIDException(string? message, Exception? innerException)
        {
        }
    }
}
