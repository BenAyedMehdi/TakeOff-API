using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Services
{
    public class CompanyService
    {
        private readonly FindYourWayDbContext _context;

        public CompanyService(FindYourWayDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _context.Companies
                .Include(c => c.Field)
                .Include(c => c.Package)
                .Include(c => c.Contact)
                .Include(c => c.Milestones)
                .ThenInclude(m=>m.Goals)
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Company> GetCompany(int id)
        {
            var company = await _context.Companies
                .Include(c => c.Field)
                .Include(c => c.Package)
                .Include(c=>c.Contact)
                .Include(c => c.Milestones)
                .ThenInclude(m => m.Goals)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CompanyId ==id);
            return company;
        }

        public async Task<Company> AddCompany(AddComanyRequest company)
        {
            var package = await _context.Packages.FindAsync(company.PackageId);
            if (package == null)
            {
                return null;
            }
            var field = await _context.Fields.FindAsync(company.FieldId);
            if (field == null)
            {
                return null;
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

            return  newCompany;
        }

        public async Task<Company> UpdateCompany(int id, UpdateCompanyRequest request)
        {
            if (id != request.CompanyId) { return null; }

            var oldCompany = await _context.Companies.FindAsync(id);
            if (oldCompany == null) { return null; }

            var field = await _context.Fields.FindAsync(request.FieldId);
            if (field == null) { return null; }

            var package = await _context.Packages.FindAsync(request.PackageId);
            if (package == null) { return null; }

            oldCompany.CompnayName = request.CompnayName;
            oldCompany.NumberOfEmployees = request.NumberOfEmployees;
            oldCompany.Field = field;
            oldCompany.Package = package;

            _context.Entry(oldCompany).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return oldCompany;
        }

        public async Task<Contact> AddContactToCompany(int id, AddContactRequest request)
        {
            if (id != request.OwnerId) { return null; }

            var oldCompany = await GetCompany(id);
            if (oldCompany == null) { return null; }

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

            await _context.SaveChangesAsync();
            
            return newContact;
        }
        public async Task DeleteCompany(int id)
        {
            var company = await GetCompany(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

        }
        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
