using Mashinin.IRepositories;
using Mashinin.Repositories;

namespace Mashinin
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly MakeRepository _makeRepository;
        private readonly ModelRepository _modelRepository;
        private readonly CityRepository _cityRepository;
        private readonly ColorRepository _colorRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }



        public IMakeRepository MakeRepository => _makeRepository ?? new MakeRepository(_context);
        public IModelRepository ModelRepository => _modelRepository ?? new ModelRepository(_context);
        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);
        public IColorRepository ColorRepository => _colorRepository ?? new ColorRepository(_context);


        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
