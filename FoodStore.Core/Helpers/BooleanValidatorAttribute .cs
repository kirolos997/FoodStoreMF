using System.ComponentModel.DataAnnotations;

namespace FoodStore.Core.Helpers
{
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
