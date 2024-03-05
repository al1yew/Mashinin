using Mashinin.Interfaces;

namespace Mashinin.Implementations
{
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
