using FluentValidation;

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
        public MakeUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required!");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage("TurboAzId is required!");
        }
    }
}
