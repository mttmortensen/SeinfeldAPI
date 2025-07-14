using System.ComponentModel.DataAnnotations;

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
    }
}
