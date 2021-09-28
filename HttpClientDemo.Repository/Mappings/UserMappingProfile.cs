using AutoMapper;
using HttpClientDemo.Data.Dto;
using HttpClientDemo.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Repository.Mappings
{
    [ExcludeFromCodeCoverage]
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
