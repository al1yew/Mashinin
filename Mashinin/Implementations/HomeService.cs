using Mashinin.Entities;
using Mashinin.Interfaces;
using Newtonsoft.Json;

namespace Mashinin.Implementations
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
