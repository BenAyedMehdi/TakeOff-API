using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Services
{
    public class UserService
    {
        private readonly FindYourWayDbContext _context;
        private readonly CompanyService companyService;

        public UserService(FindYourWayDbContext context, CompanyService companyService)
        {
            _context = context;
            this.companyService = companyService;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var list = await _context.Users
                .Include(u => u.Company)
                .ThenInclude(c => c.Field)
                .Include(c => c.Company)
                .ThenInclude(c => c.Package)
                .Include(c => c.Company)
                .ThenInclude(c => c.Contact)
                .Include(c => c.Company)
                .ThenInclude(c => c.Milestones)
                .ThenInclude(m=>m.Goals)
                .Include(c => c.Company)
                .ThenInclude(c => c.Products)
                .ToListAsync();
            return list;
        }
        public async Task<User> GetUserByID(int id)
        {
            var item = await _context.Users
                .Include(u => u.Company)
                .ThenInclude(c => c.Field)
                .Include(c => c.Company)
                .ThenInclude(c => c.Package)
                .Include(c => c.Company)
                .ThenInclude(c => c.Contact)
                .Include(c => c.Company)
                .ThenInclude(c => c.Milestones)
                .ThenInclude(m => m.Goals)
                .Include(c => c.Company)
                .ThenInclude(c => c.Products)
                .FirstOrDefaultAsync(u => u.UserId == id);
            return item;
        }

        public async Task<User> AddUser(AddUserRequest request)
        {
            if (request.FirstName == null || request.FirstName == string.Empty) return null;
            if (request.LastName == null || request.LastName == string.Empty) return null;
            if (request.Email == null || request.Email == string.Empty) return null;
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Position = request.Position
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
        public async Task<User> UpdateUser(int id, UpdateUserRequest request)
        {
            if (id != request.UserId) { return null; }

            var oldUser = await GetUserByID(id);
            if (oldUser == null) { return null; }

            var company = await companyService.GetCompany(request.CompanyId);

            oldUser.FirstName = request.FirstName;
            oldUser.LastName = request.LastName;
            oldUser.Email = request.Email;
            oldUser.Position = request.Position;

            oldUser.Company = company;

            _context.Entry(oldUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldUser;
        }
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
