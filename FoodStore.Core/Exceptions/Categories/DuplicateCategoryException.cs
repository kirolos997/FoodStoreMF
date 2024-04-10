namespace FoodStore.Core.Exceptions.Categories
{
    public class DuplicateCategoryException : Exception
    {
        public DuplicateCategoryException() : base()
        {
        }

        public DuplicateCategoryException(string? message) : base(message)
        {
        }

        public DuplicateCategoryException(string? message, Exception? innerException)
        {
        }
    }
}
