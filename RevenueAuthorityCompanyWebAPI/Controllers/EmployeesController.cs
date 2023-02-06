using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevenueAuthorityCompanyWebAPI.CoreRepositories;
using RevenueAuthorityCompanyWebAPI.DTO;
using RevenueAuthorityCompanyWebAPI.Models;

namespace RevenueAuthorityCompanyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _unitOfWork.Employees.All());
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetSingleEmployee(int id)
        {
            var employee = await _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return NotFound("Sorry, this employee does not exist.");
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("GetByCompanyId")]
        public async Task<IActionResult> GetEmployeesByCompany(int companyId)
        {
            var company = await _unitOfWork.Companies.GetById(companyId);
            if (company == null)
            {
                return NotFound("Sorry, this company does not exist");
            }

            var employees = await _unitOfWork.Employees.GetByCompanyId(companyId);
            if (employees.Count() == 0)
            {
                return NotFound("This company does not have any employees.");
            }
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(int CompanyId, EmployeeDTO employee)
        {
            var company = await _unitOfWork.Companies.GetById(CompanyId);
            if (company == null)
            {
                return NotFound("Sorry, the company provided does not exist.");
            }
            var newEmployee = new Employee()
            {
                Name= employee.Name,
                Address= employee.Address,
                Phone_number= employee.Phone_number,
                Email_address= employee.Email_address,
                CompanyId= employee.CompanyId
            };
            await _unitOfWork.Employees.Add(newEmployee);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPatch]
        [Route("UpdateEmployee")]

        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employee)
        {
            var existingEmployee = await _unitOfWork.Employees.GetById(id);
            if (existingEmployee == null)
            {
                return NotFound("Sorry, the employee you are trying to update does not exist.");
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Address = employee.Address;
            existingEmployee.Phone_number = employee.Phone_number;
            existingEmployee.Email_address = employee.Email_address;
            existingEmployee.CompanyId = employee.CompanyId;
            await _unitOfWork.Employees.Update(existingEmployee);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.Employees.GetById(id);
            if (employee == null)
            {
                return NotFound("Sorry, this employee does not exist.");
            }
            await _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
