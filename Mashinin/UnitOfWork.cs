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
        private readonly NumberPlateRepository _numberPlateRepository;
        private readonly ExtractedCarDetailRepository _extractedCarDetailRepository;
        private readonly ExtractedNumberRepository _extractedNumberRepository;
        private readonly TransportRepository _transportRepository;
        private readonly PartRepository _partRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }



        public IMakeRepository MakeRepository => _makeRepository ?? new MakeRepository(_context);
        public IModelRepository ModelRepository => _modelRepository ?? new ModelRepository(_context);
        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);
        public IColorRepository ColorRepository => _colorRepository ?? new ColorRepository(_context);
        public INumberPlateRepository NumberPlateRepository => _numberPlateRepository ?? new NumberPlateRepository(_context);
        public IExtractedCarDetailRepository ExtractedCarDetailRepository => _extractedCarDetailRepository ?? new ExtractedCarDetailRepository(_context);
        public IExtractedNumberRepository ExtractedNumberRepository => _extractedNumberRepository ?? new ExtractedNumberRepository(_context);
        public ITransportRepository TransportRepository => _transportRepository ?? new TransportRepository(_context);
        public IPartRepository PartRepository => _partRepository ?? new PartRepository(_context);


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
