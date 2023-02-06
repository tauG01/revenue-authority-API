using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RevenueAuthorityCompanyWebAPI.Data;
using RevenueAuthorityCompanyWebAPI.Models;
using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace RevenueAuthorityCompanyWebAPI.CoreRepositories.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApiDbContext context, ILogger logger) : base(context, logger) 
        {
        }

        //get all employees with company
        public override async Task<IEnumerable<Employee>> All()
        {
            try
            {
                return await _context.Employees.AsNoTracking()
                .Include(c => c.Company)
                .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //get an employee with company
        public override async Task<Employee?> GetById(int id)
        {
            try
            {
                return await _context.Employees.AsNoTracking()
                .Include(c => c.Company)
                .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //get all employees of a company

        public async Task<IEnumerable<Employee>> GetByCompanyId(int companyId)
        {
            try
            {
                return await _context.Employees.AsNoTracking()
                .Where(e => e.CompanyId == companyId)
                .Include(c => c.Company)
                .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
