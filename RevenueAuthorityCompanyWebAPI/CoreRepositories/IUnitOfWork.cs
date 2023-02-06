namespace RevenueAuthorityCompanyWebAPI.CoreRepositories
{
    public interface IUnitOfWork
    {
        ICompanyRepository Companies { get; }
        IEmployeeRepository Employees { get; }
        Task CompleteAsync();
    }
}
