using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SeinfeldAPI.Services.Attributes
{
    public class ValidUsernameAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public ValidUsernameAttribute(int minLength = 3, int maxLength = 50) 
        {
            _minLength = minLength;
            _maxLength = maxLength;
            ErrorMessage = $"Username must be between {_minLength}--{_maxLength} characters and can only contain letters, numbers and underscores.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;

            if (string.IsNullOrWhiteSpace(username))
                return new ValidationResult("Username is required.");

            if (username.Length < _minLength || username.Length > _maxLength)
                return new ValidationResult($"Username must be between {_minLength} and {_maxLength} characters.");

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
                return new ValidationResult("Username can only contain letters, numbers, and underscores.");

            return ValidationResult.Success;
        }
    }
}
