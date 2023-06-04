namespace AspNewsAPI.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
    }
}
