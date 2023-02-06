using Microsoft.EntityFrameworkCore;
using RevenueAuthorityCompanyWebAPI.Models;

namespace RevenueAuthorityCompanyWebAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
