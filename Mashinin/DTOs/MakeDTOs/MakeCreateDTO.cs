using FluentValidation;

namespace Mashinin.DTOs.MakeDTOs
{
    public class MakeCreateDTO
    {
        public int TurboAzId { get; set; }
        public string Name { get; set; }
    }

    public class MakeCreateDTOValidator : AbstractValidator<MakeCreateDTO>
    {
        public MakeCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required!");

            RuleFor(x => x.TurboAzId)
                .NotEmpty().WithMessage("TurboAzId is required!");
        }
    }
}
