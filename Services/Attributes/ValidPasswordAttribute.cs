using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SeinfeldAPI.Services.Attributes
{
    public class ValidPasswordAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public ValidPasswordAttribute(int minLength = 8)
        {
            _minLength = minLength;
            ErrorMessage = $"Password must be at least {_minLength} characters and contain an uppercase, lowercase, digit, and special character.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("Password is required.");

            if (password.Length < _minLength)
                return new ValidationResult($"Password must be at least {_minLength} characters.");

            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$"))
                return new ValidationResult("Password must contain an uppercase, lowercase, digit, and special character.");

            return ValidationResult.Success;
        }
    }
}
