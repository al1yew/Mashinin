using FluentValidation;
using Mashinin.Localization;
using Microsoft.Extensions.Localization;

namespace Mashinin.DTOs.ModelDTOs
{
    public class ModelUpdateDTO
    {
        public int Id { get; set; }
        public int TurboAzId { get; set; }
        public string Name { get; set; }
        public string? Class { get; set; }
        public int MakeId { get; set; }
    }

    public class ModelUpdateDTOValidator : AbstractValidator<ModelUpdateDTO>
    {
        public ModelUpdateDTOValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage(x => "Id " + stringLocalizer["required"])
                .GreaterThan(0).WithMessage(x => "Id " + stringLocalizer["mustBeGreaterThanZero"]);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => stringLocalizer["nameRequired"]);

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage(x => "TurboAzId " + stringLocalizer["required"])
                .GreaterThan(0).WithMessage(x => "TurboAzId " + stringLocalizer["mustBeGreaterThanZero"]);

            RuleFor(x => x.MakeId)
               .NotEmpty().WithMessage(x => "MakeId " + stringLocalizer["required"])
                .GreaterThan(0).WithMessage(x => "MakeId " + stringLocalizer["mustBeGreaterThanZero"]);
        }
    }
}
