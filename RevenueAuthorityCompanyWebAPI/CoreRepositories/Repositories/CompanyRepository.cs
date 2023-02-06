using Microsoft.EntityFrameworkCore;
using RevenueAuthorityCompanyWebAPI.Data;
using RevenueAuthorityCompanyWebAPI.Models;

namespace RevenueAuthorityCompanyWebAPI.CoreRepositories.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApiDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<Company?> GetById(int id)
        {
            try
            {
                return await _context.Companies.AsNoTracking()
                    .Where(c => c.Id == id)
                    .Include(v => v.Employees)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
