using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;

        public PackagesController(FindYourWayDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all Packages 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetAllPackages()
        {
            var list = await _context.Packages.ToListAsync();
            return Ok(list);
        }



        /// <summary>
        /// Returns a package by specifying its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackageByID(int id)
        {
            if (id == 0) return BadRequest();
            var item = await _context.Packages.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Add package
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddPackage([FromBody] AddPackageRequest request)
        {
            if (request.PackageName == null || request.PackageName == string.Empty) return BadRequest();
            var newPackage = new Package
            {
                PackageName = request.PackageName,
                Price = request.Price,
                Description = request.Description
            };
            await _context.Packages.AddAsync(newPackage);
            await _context.SaveChangesAsync();
            return Ok(newPackage);
        }
        /// <summary>
        /// Delete a Package by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
