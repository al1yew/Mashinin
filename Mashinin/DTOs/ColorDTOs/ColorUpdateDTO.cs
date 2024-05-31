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
              .NotEmpty().WithMessage(x => "Id " + stringLocalizer["required"])
              .GreaterThan(0).WithMessage(x => "Id " + stringLocalizer["mustBeGreaterThanZero"]);

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
