using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.ModelDTOs
{
    public class ModelCreateDTO
    {
        public int TurboAzId { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string? Class { get; set; }
    }

    public class ModelCreateDTOValidator : AbstractValidator<ModelCreateDTO>
    {
        public ModelCreateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.MakeId)
               .NotEmpty().WithMessage(x => "MakeId " + stringLocalizer["required"]);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => stringLocalizer["nameRequired"]);

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage(x => "TurboAzId " + stringLocalizer["required"]);
        }
    }
}
