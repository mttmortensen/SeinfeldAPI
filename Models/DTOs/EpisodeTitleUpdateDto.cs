using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * PUT /api/episodes/{id}/title
     *
     * Only updates the title. The existing PUT /api/episodes/{id}
     * requires Season and EpisodeNumber to be sent as well.
     */
    public class EpisodeTitleUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
    }
}
