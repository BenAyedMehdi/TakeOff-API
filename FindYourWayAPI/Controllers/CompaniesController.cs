using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using FindYourWayAPI.Services;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _companyService;

        public CompaniesController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Companies
        /// <summary>
        /// Get All companies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return Ok( await _companyService.GetCompanies());
        }


        // GET: api/Companies/5
        /// <summary>
        /// Returns a company by specifing its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompany(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }


        /// <summary>
        /// Create a new company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        // POST: api/Companies
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(AddComanyRequest company)
        {
            var newCompany = await _companyService.AddCompany(company);

            return CreatedAtAction("GetCompany", new { id = newCompany.CompanyId }, newCompany);
        }

        
        // PUT: api/Companies/5
        /// <summary>
        /// Update the state of a company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, UpdateCompanyRequest request)
        {
            if (!_companyService.CompanyExists(id)) return NotFound();
            var company = await _companyService.UpdateCompany(id, request);
            return CreatedAtAction("GetCompany", new { id = id }, company);
        }


        // PUT: api/Companies/5
        /// <summary>
        /// Add a contact to a company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("AddContact/{id}")]
        public async Task<ActionResult<Contact>> AddContactToCompany(int id, AddContactRequest request)
        {
            if (!_companyService.CompanyExists(id)) return NotFound();

            var newContact = await _companyService.AddContactToCompany(id, request);
            if (newContact == null) return BadRequest();
            return Ok(newContact);
        }

        // DELETE: api/Companies/5
        /// <summary>
        /// Delete a company by passing its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (!_companyService.CompanyExists(id))
            {
                return NotFound();
            }
            await _companyService.DeleteCompany(id);
            return NoContent();
        }

        
    }
}
