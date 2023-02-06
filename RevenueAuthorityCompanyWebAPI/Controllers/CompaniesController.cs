using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueAuthorityCompanyWebAPI.CoreRepositories;
using RevenueAuthorityCompanyWebAPI.Data;
using RevenueAuthorityCompanyWebAPI.DTO;
using RevenueAuthorityCompanyWebAPI.Models;
using System.Text.Json;

namespace RevenueAuthorityCompanyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompaniesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _unitOfWork.Companies.All();
            return Ok(companies);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetSingleCompany(int id)
        {
            var company = await _unitOfWork.Companies.GetById(id);
            if (company == null)
            {
                return NotFound("Sorry, this company does not exist.");
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyDTO companyDTO)
        {
            var newCompany = new Company()
            {
                Name = companyDTO.Name,
                Address = companyDTO.Address,
                Phone_number = companyDTO.Phone_number,
                Email_address = companyDTO.Email_address,
            };
            await _unitOfWork.Companies.Add(newCompany);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(AddCompany), new { id = newCompany.Id }, newCompany);
        }

        [HttpPatch]
        [Route("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyDTO companyDTO)
        {
            var existingCompany = await _unitOfWork.Companies.GetById(id);
            if (existingCompany == null)
            {
                return NotFound("Sorry, the company you are trying to update does not exist.");
            }
            existingCompany.Name = companyDTO.Name;
            existingCompany.Address = companyDTO.Address;
            existingCompany.Phone_number = companyDTO.Phone_number;
            existingCompany.Email_address = companyDTO.Email_address;
            await _unitOfWork.Companies.Update(existingCompany);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _unitOfWork.Companies.GetById(id);
            if (company == null)
            {
                return NotFound("Sorry, this company does not exist.");
            }
            await _unitOfWork.Companies.Delete(company);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
