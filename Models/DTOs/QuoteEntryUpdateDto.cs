using System.ComponentModel.DataAnnotations;

namespace SeinfeldAPI.Models.DTOs
{
    /*
     * PUT /api/quotes/{id}
     *
     * Updates the text and speaker of a quote.
     * Moving a quote to another episode is not done here.
     */
    public class QuoteEntryUpdateDto
    {
        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        [Required]
        [StringLength(100)]
        public string Speaker { get; set; }
    }
}
