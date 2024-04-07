using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Product> products { get; set; }

    }
}
