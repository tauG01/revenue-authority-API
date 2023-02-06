using System.Text.Json.Serialization;

namespace RevenueAuthorityCompanyWebAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty;
        public string Phone_number { get; set; } = string.Empty;
        public string Email_address { get; set; } = string.Empty;

        //Relationship

        [JsonIgnore]
        public List<Employee> Employees { get; set; }

    }
}
