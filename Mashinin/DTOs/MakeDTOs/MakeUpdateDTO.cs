using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.MakeDTOs
{
    public class MakeUpdateDTO
    {
        public int Id { get; set; }
        public int TurboAzId { get; set; }
        public string Name { get; set; }
    }

    public class MakeUpdateDTOValidator : AbstractValidator<MakeUpdateDTO>
    {
        public MakeUpdateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage(x => "Id " + stringLocalizer["required"]);

            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage(x => stringLocalizer["nameRequired"]);

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage(x => "TurboAzId " + stringLocalizer["required"]);
        }
    }
}
