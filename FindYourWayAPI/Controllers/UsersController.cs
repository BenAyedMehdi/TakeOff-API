using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using FindYourWayAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;
        private readonly CompanyService companyService;
        private readonly UserService _userService;

        public UsersController(FindYourWayDbContext context, UserService userService, CompanyService companyService)
        {
            _context = context;
            this.companyService = companyService;
            _userService = userService;
        }

        /// <summary>
        /// Returns all Users 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var list = await _userService.GetUsers();      
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
            var item = await _userService.GetUserByID(id);
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
            var newUser = await _userService.AddUser(request);
            if (newUser == null) return BadRequest();
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
            if (!_userService.UserExists(id)) return NotFound();
            
            var newUser = await _userService.UpdateUser(id, request);
            if (newUser == null) return BadRequest();

            return Ok(newUser);
        }
       
    }
}
