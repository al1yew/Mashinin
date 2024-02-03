using AutoMapper;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.Entities;

namespace Mashinin.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Make

            CreateMap<Make, MakeGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")));

            CreateMap<MakeCreateDTO, Make>()
                .ForMember(des => des.Name, src => src.MapFrom(x => x.Name.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion
        }
    }
}
