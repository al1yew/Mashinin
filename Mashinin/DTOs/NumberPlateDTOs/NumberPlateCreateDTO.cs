using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.NumberPlateDTOs
{
    public class NumberPlateCreateDTO
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }

    public class NumberPlateCreateDTOValidator : AbstractValidator<NumberPlateCreateDTO>
    {
        public NumberPlateCreateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Value)
               .NotEmpty().WithMessage(x => stringLocalizer["numberPlateRequired"])
               .Matches(@"^\d{2}[A-Z]{2}\d{3}$").WithMessage(x => stringLocalizer["numberPlateFalseFormat"]);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(x => stringLocalizer["descriptionRequired"]);
        }
    }
}
