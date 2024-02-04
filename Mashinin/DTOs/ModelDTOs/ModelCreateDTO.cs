using FluentValidation;

namespace Mashinin.DTOs.ModelDTOs
{
    public class ModelCreateDTO
    {
        public int TurboAzId { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
    }

    public class ModelCreateDTOValidator : AbstractValidator<ModelCreateDTO>
    {
        public ModelCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage("TurboAzId is required!");

            RuleFor(x => x.MakeId)
               .NotEmpty().WithMessage("MakeId is required!");
        }
    }
}
