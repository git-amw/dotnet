using AMD.Services.Funds.DomainLayer.Entites;
using AutoMapper;

namespace AMD.Services.Funds.DomainLayer.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionHistoryEntity, TransactionHistoryReceiptEntity>()
                .ForMember(dest => dest.ReceiptType, opt => opt.MapFrom(src => src.TransactionType));  
        }
    }
}
