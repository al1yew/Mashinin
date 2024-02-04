using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.MakeDTOs
{
    public class MakeCreateDTO
    {
        public int TurboAzId { get; set; }
        public string Name { get; set; }
    }

    public class MakeCreateDTOValidator : AbstractValidator<MakeCreateDTO>
    {
        public MakeCreateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(x => stringLocalizer["nameRequired"]);

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage(x => "TurboAzId " + stringLocalizer["required"]);
        }
    }
}
