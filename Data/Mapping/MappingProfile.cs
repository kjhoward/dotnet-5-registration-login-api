using RegistrationLoginApi.Data.DataModels;
using DevConsulting.RegistrationLoginApi.Client;
using RegistrationLoginApi.Models.Users;
namespace RegistrationLoginApi.Data.Mapping{
    public class MappingProfile : AutoMapper.Profile{
        public MappingProfile(){
            CreateMap<User, UserResource>();
            CreateMap<UserResource, User>();
            CreateMap<UserResource, AuthenticateResponse>();
        }
    }

}