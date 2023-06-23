using System.ComponentModel.DataAnnotations;

namespace AspNewsAPI.DTOs
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
