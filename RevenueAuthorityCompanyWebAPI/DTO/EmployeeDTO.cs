namespace RevenueAuthorityCompanyWebAPI.DTO
{
    public class EmployeeDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone_number { get; set; } = string.Empty;
        public string Email_address { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}
