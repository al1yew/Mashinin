using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.NumberPlateDTOs
{
    public class NumberPlateUpdateDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsForBargain { get; set; }
    }

    public class NumberPlateUpdateDTOValidator : AbstractValidator<NumberPlateUpdateDTO>
    {
        public NumberPlateUpdateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(x => "Id " + stringLocalizer["required"]);

            RuleFor(x => x.Value)
              .NotEmpty().WithMessage(x => stringLocalizer["numberPlateRequired"])
              .Matches(@"^\d{2}[A-Z]{2}\d{3}$").WithMessage(x => stringLocalizer["numberPlateFalseFormat"]);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(x => stringLocalizer["descriptionRequired"]);
        }
    }
}
