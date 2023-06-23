using AspNewsAPI.DTOs;
using AspNewsAPI.Entities;
using AutoMapper;

namespace AspNewsAPI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //author
            CreateMap<AuthorCreationDTO, Author>();

            CreateMap<Author, AuthorDTO>();

            //category
            CreateMap<CategoryCreationDTO, Category>();

            CreateMap<Category, CategoryDTO>();

            //news
            CreateMap<NewsCreationDTO, News>();
            CreateMap<News, NewsDTO>();
        }
    }
}
