using AutoMapper;
using Mashinin.DTOs.CityDTOs;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.DTOs.ExtractedCarDetailDTOs;
using Mashinin.DTOs.ExtractedNumberDTOs;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.DTOs.ModelDTOs;
using Mashinin.DTOs.NumberPlateDTOs;
using Mashinin.DTOs.TransportDTOs;
using Mashinin.Entities;

namespace Mashinin.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Make

            CreateMap<Make, MakeGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForPath(des => des.Models, src => src.MapFrom(x => x.Models));

            CreateMap<MakeCreateDTO, Make>()
                .ForMember(des => des.Name, src => src.MapFrom(x => x.Name.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region Model

            CreateMap<Model, ModelGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.Make, src => src.MapFrom(x => x.Make.Name))
                .ForMember(des => des.MakeTurboAzId, src => src.MapFrom(x => x.Make.TurboAzId))
                .ForMember(des => des.MakeId, src => src.MapFrom(x => x.Make.Id));

            CreateMap<ModelCreateDTO, Model>()
                .ForMember(des => des.Name, src => src.MapFrom(x => x.Name.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region City

            CreateMap<City, CityGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")));

            CreateMap<CityCreateDTO, City>()
                .ForMember(des => des.NameRu, src => src.MapFrom(x => x.NameRu.Trim()))
                .ForMember(des => des.NameAz, src => src.MapFrom(x => x.NameAz.Trim()))
                .ForMember(des => des.NameEn, src => src.MapFrom(x => x.NameEn.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region Color

            CreateMap<Color, ColorGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")));

            CreateMap<ColorCreateDTO, Color>()
                .ForMember(des => des.NameRu, src => src.MapFrom(x => x.NameRu.Trim()))
                .ForMember(des => des.NameAz, src => src.MapFrom(x => x.NameAz.Trim()))
                .ForMember(des => des.NameEn, src => src.MapFrom(x => x.NameEn.Trim()))
                .ForMember(des => des.HexCode, src => src.MapFrom(x => x.HexCode.Trim().ToUpperInvariant()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region NumberPlate

            CreateMap<NumberPlate, NumberPlateGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")));

            CreateMap<NumberPlateCreateDTO, NumberPlate>()
                .ForMember(des => des.Value, src => src.MapFrom(x => x.Value.Trim().ToUpperInvariant()))
                .ForMember(des => des.Description, src => src.MapFrom(x => x.Description.Trim()))
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow.AddHours(4)));

            #endregion

            #region ExtractedCarDetail

            CreateMap<ExtractedCarDetail, ExtractedCarDetailGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(des => des.PostCreatedAt, src => src.MapFrom(x => x.PostCreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")));

            #endregion

            #region ExtractedCarDetail

            CreateMap<ExtractedNumber, ExtractedNumberGetDTO>()
                .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")));

            #endregion

            #region Transport

            CreateMap<Transport, TransportGetDTO>()
       /*time*/ .ForMember(des => des.CreatedAt, src => src.MapFrom(x => x.CreatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
       /*time*/ .ForMember(des => des.DeletedAt, src => src.MapFrom(x => x.DeletedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
       /*time*/ .ForMember(des => des.UpdatedAt, src => src.MapFrom(x => x.UpdatedAt.Value.ToString("dd.MM.yyyy HH:mm:ss")))
       /*time*/ .ForMember(des => des.ValidFrom, src => src.MapFrom(x => x.ValidFrom.Value.ToString("dd.MM.yyyy HH:mm:ss")))
       /*time*/ .ForMember(des => des.ValidUntil, src => src.MapFrom(x => x.ValidUntil.Value.ToString("dd.MM.yyyy HH:mm:ss")))
       /*time*/ .ForMember(des => des.VipExpireDate, src => src.MapFrom(x => x.VipExpireDate.Value.ToString("dd.MM.yyyy HH:mm:ss")))

       /*make*/ .ForMember(des => des.MakeName, src => src.MapFrom(x => x.Make.Name))
       /*make*/ .ForMember(des => des.MakeTurboAzId, src => src.MapFrom(x => x.Make.TurboAzId))

       /*model*/.ForMember(des => des.ModelTurboAzId, src => src.MapFrom(x => x.Model.TurboAzId))
       /*model*/.ForMember(des => des.ModelName, src => src.MapFrom(x => x.Model.Name))
       /*model*/.ForMember(des => des.ModelClass, src => src.MapFrom(x => x.Model.Class))

       /*city*/ .ForMember(des => des.CityNameAz, src => src.MapFrom(x => x.City.NameAz))
       /*city*/ .ForMember(des => des.CityNameRu, src => src.MapFrom(x => x.City.NameRu))
       /*city*/ .ForMember(des => des.CityNameEn, src => src.MapFrom(x => x.City.NameEn))

       /*color*/.ForMember(des => des.ColorHexCode, src => src.MapFrom(x => x.Color.HexCode))
       /*color*/.ForMember(des => des.ColorNameAz, src => src.MapFrom(x => x.Color.NameAz))
       /*color*/.ForMember(des => des.ColorNameRu, src => src.MapFrom(x => x.Color.NameRu))
       /*color*/.ForMember(des => des.ColorNameEn, src => src.MapFrom(x => x.Color.NameEn))

                .ForPath(des => des.Prices, src => src.MapFrom(x => x.Prices))
                .ForPath(des => des.TransportImages, src => src.MapFrom(x => x.TransportImages));


            #endregion
        }
    }
}
