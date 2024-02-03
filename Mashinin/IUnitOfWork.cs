using Mashinin.IRepositories;

namespace Mashinin
{
    public interface IUnitOfWork
    {
        IModelRepository ModelRepository { get; }
        IMakeRepository MakeRepository { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
