using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin.Repositories
{
    public class TransportRepository : Repository<Transport>, ITransportRepository
    {
        public TransportRepository(AppDbContext context) : base(context)
        {

        }
    }
}
