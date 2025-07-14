using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SeinfeldAPI.Services.Attributes
{
    public class ValidSeasonOrEpisodeNumber : ValidationAttribute
    {
        public enum ValidationType { Season, EpisodeNumber }

        private readonly ValidationType _type;

        public ValidSeasonOrEpisodeNumber(ValidationType type)
        {
            _type = type;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
                return new ValidationResult($"{_type} is required.");

            if(_type == ValidationType.Season) 
            {
                if (!Regex.IsMatch(str, @"^S[1-9]$"))
                    return new ValidationResult("Season must be in the format 'S1' to 'S9' with no leading zeros.");
            }
            else if (_type == ValidationType.EpisodeNumber)
            {
                if (!Regex.IsMatch(str, @"^E([1-9]|[1-2][0-9]|30)$"))
                    return new ValidationResult("EpisodeNumber must be in the format 'E1' to 'E30' with no leading zeros.");
            }

            return ValidationResult.Success;
        }
    }
}
