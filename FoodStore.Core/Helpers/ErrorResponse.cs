namespace FoodStore.Core.Helpers
{
    /// <summary>
    /// Error respone class to be passed in case an exception/erro raised.
    /// </summary>
    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string? ErrorType { get; set; }

        public object? Message { get; set; }

    }
}
