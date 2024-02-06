using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.ColorDTOs
{
    public class ColorUpdateDTO
    {
        public int Id { get; set; }
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public string HexCode { get; set; }
    }
    public class ColorUpdateDTOValidator : AbstractValidator<ColorUpdateDTO>
    {
        public ColorUpdateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage(x => "Id " + stringLocalizer["required"]);

            RuleFor(x => x.NameAz)
             .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameEn)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameRu)
              .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.HexCode)
              .NotEmpty().WithMessage(x => "HexCode " + stringLocalizer["required"]);
        }
    }
}
