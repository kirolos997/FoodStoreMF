namespace FoodStore.Core.Helpers
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string? ErrorType { get; set; }

        public object? Message { get; set; }

    }
}
