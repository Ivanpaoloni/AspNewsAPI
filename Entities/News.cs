using System.ComponentModel.DataAnnotations;

namespace AspNewsAPI.Entities
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        [type: Url]
        public string ImageUrl { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
