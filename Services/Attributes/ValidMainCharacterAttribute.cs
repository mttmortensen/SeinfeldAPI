using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Services.Attributes
{
    public class ValidMainCharacterAttribute  : ValidationAttribute
    {
        private static readonly string[] MainCharacters =
        {
            "Jerry", "George", "Elanie", "Kramer"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var character = value as string;

            if (string.IsNullOrWhiteSpace(character))
                return new ValidationResult("Character name is required. ");

            if(!MainCharacters.Contains(character))
                return new ValidationResult($"'{character}' is not a main character. Allowed: {string.Join(", ", MainCharacters)}");

            return ValidationResult.Success;
        }
    }
}
