using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.CityDTOs
{
    public class CityCreateDTO
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public int Index { get; set; }
    }

    public class CityCreateDTOValidator : AbstractValidator<CityCreateDTO>
    {
        public CityCreateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.NameAz)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameEn)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameRu)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.Index)
              .NotEmpty().WithMessage(x => "Index " + stringLocalizer["required"]);
        }
    }
}
