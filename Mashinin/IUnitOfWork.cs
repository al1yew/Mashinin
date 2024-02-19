using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin
{
    public interface IUnitOfWork
    {
        IModelRepository ModelRepository { get; }
        IMakeRepository MakeRepository { get; }
        ICityRepository CityRepository { get; }
        IColorRepository ColorRepository { get; }
        INumberPlateRepository NumberPlateRepository { get; }
        IExtractedCarDetailRepository ExtractedCarDetailRepository { get; }
        IExtractedNumberRepository ExtractedNumberRepository { get; }

        Task<int> CommitAsync();
        int Commit();
    }
}
