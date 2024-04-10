namespace FoodStore.Core.Helpers
{
    /// <summary>
    /// Attribute class used to identify what attributes can be searched 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SearchableAttribute : Attribute
    {
    }
}
