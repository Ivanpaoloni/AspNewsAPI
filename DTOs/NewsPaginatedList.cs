using AspNewsAPI.Entities;

namespace AspNewsAPI.DTOs
{
    public class NewsPaginatedList<T>
    {   
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
