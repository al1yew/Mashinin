using AutoMapper;
using Mashinin.DTOs.CityDTOs;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.DTOs.ExtractedCarDetailDTOs;
using Mashinin.DTOs.ExtractedNumberDTOs;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.DTOs.ModelDTOs;
using Mashinin.DTOs.NumberPlateDTOs;
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
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForPath(des => des.Models, src => src.MapFrom(x => x.Models));

            CreateMap<MakeCreateDTO, Make>()
                .ForMember(des => des.Name, src => src.MapFrom(x => x.Name.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region Model

            CreateMap<Model, ModelGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.Make, src => src.MapFrom(x => x.Make.Name))
                .ForMember(des => des.MakeTurboAzId, src => src.MapFrom(x => x.Make.TurboAzId))
                .ForMember(des => des.MakeId, src => src.MapFrom(x => x.Make.Id));

            CreateMap<ModelCreateDTO, Model>()
                .ForMember(des => des.Name, src => src.MapFrom(x => x.Name.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region City

            CreateMap<City, CityGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")));

            CreateMap<CityCreateDTO, City>()
                .ForMember(des => des.NameRu, src => src.MapFrom(x => x.NameRu.Trim()))
                .ForMember(des => des.NameAz, src => src.MapFrom(x => x.NameAz.Trim()))
                .ForMember(des => des.NameEn, src => src.MapFrom(x => x.NameEn.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region Color

            CreateMap<Color, ColorGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")));

            CreateMap<ColorCreateDTO, Color>()
                .ForMember(des => des.NameRu, src => src.MapFrom(x => x.NameRu.Trim()))
                .ForMember(des => des.NameAz, src => src.MapFrom(x => x.NameAz.Trim()))
                .ForMember(des => des.NameEn, src => src.MapFrom(x => x.NameEn.Trim()))
                .ForMember(des => des.HexCode, src => src.MapFrom(x => x.HexCode.Trim().ToUpperInvariant()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region NumberPlate

            CreateMap<NumberPlate, NumberPlateGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")));

            CreateMap<NumberPlateCreateDTO, NumberPlate>()
                .ForMember(des => des.Value, src => src.MapFrom(x => x.Value.Trim().ToUpperInvariant()))
                .ForMember(des => des.Description, src => src.MapFrom(x => x.Description.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region ExtractedCarDetail

            CreateMap<ExtractedCarDetail, ExtractedCarDetailGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")))
                .ForMember(des => des.PostCreatedAt, src => src.MapFrom(x => x.PostCreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")));

            #endregion

            #region ExtractedCarDetail

            CreateMap<ExtractedNumber, ExtractedNumberGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy hh:mm:ss")));

            #endregion
        }
    }
}
