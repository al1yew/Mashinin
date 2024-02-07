using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.CityDTOs
{
    public class CityUpdateDTO
    {
        public int Id { get; set; }
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
    }

    public class CityUpdateDTOValidator : AbstractValidator<CityUpdateDTO>
    {
        public CityUpdateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage(x => "Id " + stringLocalizer["required"]);

            RuleFor(x => x.NameAz)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameEn)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameRu)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);
        }
    }
}
