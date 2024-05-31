using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;
using System.Xml;

namespace Mashinin.DTOs.ColorDTOs
{
    public class ColorCreateDTO
    {
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public string HexCode { get; set; }
    }
    public class ColorCreateDTOValidator : AbstractValidator<ColorCreateDTO>
    {
        public ColorCreateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.NameAz)
             .NotEmpty().WithMessage(x => "NameAz " + stringLocalizer["required"]);

            RuleFor(x => x.NameEn)
              .NotEmpty().WithMessage(x => "NameEn " + stringLocalizer["required"]);

            RuleFor(x => x.NameRu)
              .NotEmpty().WithMessage(x => "NameRu " + stringLocalizer["required"]);

            RuleFor(x => x.HexCode)
              .NotEmpty().WithMessage(x => "HexCode " + stringLocalizer["required"])
              .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$").WithMessage(x => stringLocalizer["hexCodeNotMatchFormat"]);
        }
    }
}
