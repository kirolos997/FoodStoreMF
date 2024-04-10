using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.Helpers
{
    /// <summary>
    ///  Boolean validator attribute to check in data binding whether or not the passed value is a true boolean
    /// </summary>
    public class BooleanValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null && value is bool)
            {
                return ValidationResult.Success;

            }
            return new ValidationResult(errorMessage: "Invalid boolean value. Expected 'true' or 'false'");
        }
    }
}
