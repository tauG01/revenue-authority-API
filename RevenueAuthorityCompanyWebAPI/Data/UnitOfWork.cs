using RevenueAuthorityCompanyWebAPI.CoreRepositories;
using RevenueAuthorityCompanyWebAPI.CoreRepositories.Repositories;

namespace RevenueAuthorityCompanyWebAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiDbContext _context;
        public ICompanyRepository Companies { get; private set; }
        public IEmployeeRepository Employees { get; private set; }

        public UnitOfWork(ApiDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var _logger = loggerFactory.CreateLogger("logs");

            Companies = new CompanyRepository(_context, _logger);
            Employees= new EmployeeRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
