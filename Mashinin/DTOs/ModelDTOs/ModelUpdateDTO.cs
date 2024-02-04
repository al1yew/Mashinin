using FluentValidation;

namespace Mashinin.DTOs.ModelDTOs
{
    public class ModelUpdateDTO
    {
        public int Id { get; set; }
        public int TurboAzId { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
    }

    public class ModelUpdateDTOValidator : AbstractValidator<ModelUpdateDTO>
    {
        public ModelUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("Id is required!");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage("TurboAzId is required!");

            RuleFor(x => x.MakeId)
               .NotEmpty().WithMessage("MakeId is required!");
        }
    }
}
