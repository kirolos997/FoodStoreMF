namespace FoodStore.Core.DTO.QueryFilters
{
    public class FilterTerm
    {
        public string Name { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public bool ValidSyntax { get; set; }
    }
}
