using Mashinin.Entities;
using Mashinin.IRepositories;

namespace Mashinin.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {

        }
    }
}
