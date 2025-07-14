using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Services.Attributes
{
    public class ValidSeinfeldTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var title = value as string;

            if (string.IsNullOrWhiteSpace(title))
                return new ValidationResult("Title Is Required.");

            /*
             * The shortest episode title in Seinfeld is "The Deal" at 8 characters
             * The longest episode title in Seinfeld is "The Puerto Rican Day" at 21 characters
             */
            if (title.Length < 8 || title.Length > 21)
                return new ValidationResult("Title must be between 8 and 21 characters.");


            /*
             * All episodes start with 'The '
             */
            if (!title.StartsWith("The "))
                return new ValidationResult("Title must start with 'The '.");

            return ValidationResult.Success;
        }
    }
}
