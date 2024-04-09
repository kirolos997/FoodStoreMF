namespace FoodStore.Core.Exceptions.Generic
{
    public class InvalidOperatorException : ArgumentException
    {
        public InvalidOperatorException() : base()
        {
        }

        public InvalidOperatorException(string? message) : base(message)
        {
        }

        public InvalidOperatorException(string? message, Exception? innerException)
        {
        }
    }
}
