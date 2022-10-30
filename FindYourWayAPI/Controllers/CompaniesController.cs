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

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;

        public CompaniesController(FindYourWayDbContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        /// <summary>
        /// Get All companies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies
                .Include(c=>c.Field)
                .Include(c=>c.Package)
                .Include(c => c.Contact)
                .Include(c=>c.Milestones)
                .ToListAsync();
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
            var company = await _context.Companies.
                Include(c => c.Field)
                .Include(c=>c.Package)
                .Include(c=>c.Milestones)
                .FirstOrDefaultAsync(c=>c.CompanyId == id);

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
            var package = await _context.Packages.FindAsync(company.PackageId);
            if (package == null)
            {
                return BadRequest();
            }
            var field = await _context.Fields.FindAsync(company.FieldId);
            if(field == null)
            {
                return BadRequest();
            }
            var newCompany = new Company
            {
                CompnayName = company.CompnayName,
                NumberOfEmployees = company.NumberOfEmployees,
                Field = field,
                Package = package
            };
            _context.Companies.Add(newCompany);
            await _context.SaveChangesAsync();

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
            if (id != request.CompanyId) {return BadRequest(); }
            
            var oldCompany = await _context.Companies.FindAsync(id);
            if (oldCompany == null) {return BadRequest();}
            
            var field = await _context.Fields.FindAsync(request.FieldId);
            if (field == null) { return BadRequest();}

            var package = await _context.Packages.FindAsync(request.PackageId);
            if (package == null) { return BadRequest(); }

            oldCompany.CompnayName = request.CompnayName;
            oldCompany.NumberOfEmployees = request.NumberOfEmployees;
            oldCompany.Field = field;
            oldCompany.Package = package;

            _context.Entry(oldCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompany", new { id = id }, oldCompany);
        }


        // PUT: api/Companies/5
        /// <summary>
        /// Add a contact to a company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("AddContact/{id}")]
        public async Task<IActionResult> AddContactToCompany(int id, AddContactRequest request)
        {
            if (id != request.OwnerId) { return BadRequest(); }

            var oldCompany = await _context.Companies.FindAsync(id);
            if (oldCompany == null) { return BadRequest(); }

            var newContact = new Contact
            {
                Email = request.Email,
                Adress = request.Adress,
                PhoneNumber = request.PhoneNumber,
                Website = request.Website,
                OwnerId = request.OwnerId
            };
            await _context.Contacts.AddAsync(newContact);   
            await _context.SaveChangesAsync();

            oldCompany.Contact = newContact;

            _context.Entry(oldCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
