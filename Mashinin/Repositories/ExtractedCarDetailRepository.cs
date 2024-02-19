using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin.Repositories
{
    public class ExtractedCarDetailRepository : Repository<ExtractedCarDetail>, IExtractedCarDetailRepository
    {
        public ExtractedCarDetailRepository(AppDbContext context) : base(context)
        {

        }
    }
}
