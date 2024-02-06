using Mashinin.IRepositories;

namespace Mashinin
{
    public interface IUnitOfWork
    {
        IModelRepository ModelRepository { get; }
        IMakeRepository MakeRepository { get; }
        ICityRepository CityRepository { get; }
        IColorRepository ColorRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
