using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin.Repositories
{
    public class NumberPlateRepository : Repository<NumberPlate>, INumberPlateRepository
    {
        public NumberPlateRepository(AppDbContext context) : base(context)
        {

        }
    }
}
