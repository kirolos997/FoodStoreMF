using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.DTO.Pagination
{
    public class Pagination
    {
        [Range(1, int.MaxValue, ErrorMessage = "Pagination parameter'Limit' must be positive integer greater than 1")]
        public int Limit { get; set; } = int.MaxValue;

        [Range(0, double.MaxValue, ErrorMessage = "Pagination parameter'Limit' must be positive integer")]
        public int Offset { get; set; } = 0;
    }
}
