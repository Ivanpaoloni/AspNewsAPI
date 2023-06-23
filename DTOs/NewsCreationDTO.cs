using System.ComponentModel.DataAnnotations;

namespace AspNewsAPI.DTOs
{
    public class NewsCreationDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        [Url]
        public string ImageUrl { get; set; }
        [Required]
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
