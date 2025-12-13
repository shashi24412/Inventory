using AutoMapper;
using Business.ViewModel;
using Data.Entity;

namespace Business.Configuration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<INVM_ItemMaster, INVM_ItemMasterViewModel>().ReverseMap();
    }
}