using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin.Repositories
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        public PartRepository(AppDbContext context) : base(context)
        {

        }
    }
}
