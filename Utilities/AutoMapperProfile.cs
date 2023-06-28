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
            CreateMap<NewsUpdateDTO, News>();
            CreateMap<News, NewsDTO>()
                .ForMember(newsDTO => newsDTO.Category, options => options.MapFrom(MapNewsDTOCategory))
            .ForMember(newsDTO => newsDTO.Author, options => options.MapFrom(MapNewsDTOAuthor));
            CreateMap<NewsPatchDTO, News>().ReverseMap();
        }


        private CategoryDTO MapNewsDTOCategory(News news, NewsDTO newsDTO)
        {
            var result = new CategoryDTO();
            if(news.CategoryId == null) {return result;}
            result.Id = news.CategoryId;
            result.Name = news.Category.Name;
            return result;

        }
        private AuthorDTO MapNewsDTOAuthor(News news, NewsDTO newsDTO)
        {
            var result = new AuthorDTO();
            if(news.AuthorId == null) {return result;}
            result.Id = news.AuthorId;
            result.Name = news.Author.Name;
            return result;
        }
    }
}
