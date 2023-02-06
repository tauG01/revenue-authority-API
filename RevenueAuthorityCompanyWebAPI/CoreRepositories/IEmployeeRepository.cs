using Microsoft.EntityFrameworkCore;
using RevenueAuthorityCompanyWebAPI.DTO;
using RevenueAuthorityCompanyWebAPI.Models;
using System.Linq.Expressions;

namespace RevenueAuthorityCompanyWebAPI.CoreRepositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByCompanyId(int companyId);
    }
}

