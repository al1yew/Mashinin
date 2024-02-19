using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin.Repositories
{
    public class ExtractedNumberRepository : Repository<ExtractedNumber>, IExtractedNumberRepository
    {
        public ExtractedNumberRepository(AppDbContext context) : base(context)
        {

        }
    }
}
