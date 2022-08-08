using AutoMapper;
using TwitterWebApi.Dtos;
using TwitterWebApi.Models;

namespace TwitterWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TweetMaster, TweetMasterDto>();
            CreateMap<UserRegister, UserDto>();
        }
    }
}
