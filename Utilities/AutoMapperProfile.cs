using AspNewsAPI.DTOs;
using AspNewsAPI.Entities;
using AutoMapper;

namespace AspNewsAPI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthorCreationDTO, Author>();
            CreateMap<CategoryCreationDTO, Category>();
        }
    }
}
