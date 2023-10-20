using AMD.Services.Accounts.DomainLayer.DTOs;
using AMD.Services.Accounts.DomainLayer.Entities;
using AutoMapper;

namespace AMD.Services.Accounts.DomainLayer.Helper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegistrationDTO, RegistrationEntity>();
            CreateMap<LoginDTO, LoginEntity>();
        }
    }
}
