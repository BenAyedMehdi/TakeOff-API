using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;

        public UsersController(FindYourWayDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all Users 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var list = await _context.Users
                .Include(u => u.Company)
                .ThenInclude(c => c.Field)
                .Include(c => c.Company)
                .ThenInclude(c => c.Package)
                .ToListAsync();
            return Ok(list);
        }



        /// <summary>
        /// Returns a user by specifying its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByID(int id)
        {
            if (id == 0) return BadRequest();
            var item = await _context.Users
                .Include(u=>u.Company)
                .ThenInclude(c=>c.Field)
                .Include(c=>c.Company)
                .ThenInclude(c=>c.Package)
                .FirstOrDefaultAsync(u => u.UserId== id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Add a new user (before the company)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] AddUserRequest request)
        {
            if (request.FirstName== null || request.FirstName== string.Empty) return BadRequest();
            if (request.LastName== null || request.LastName== string.Empty) return BadRequest();
            if (request.Email == null || request.Email== string.Empty) return BadRequest();
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Position = request.Position
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }

        // PUT: api/Companies/5
        /// <summary>
        /// Update the state of a user (usually to add a company)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserRequest request)
        {
            if (id != request.UserId) { return BadRequest(); }

            var oldUser = await _context.Users.Include(u=>u.Company).FirstOrDefaultAsync(u=>u.UserId==id);
            if (oldUser== null) { return BadRequest(); }

            var company = await _context.Companies
                .Include(c=>c.Package)
                .Include(c=>c.Field)
                .FirstOrDefaultAsync(c=>c.CompanyId==request.CompanyId);
            if (company == null) { return BadRequest(); }

            oldUser.FirstName= request.FirstName;
            oldUser.LastName= request.LastName;
            oldUser.Email= request.Email;
            oldUser.Position= request.Position;

            oldUser.Company= company;

            _context.Entry(oldUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(request);
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId== id);
        }
    }
}
