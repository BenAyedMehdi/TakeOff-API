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
using FindYourWayAPI.Services;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestonesController : ControllerBase
    {
        private readonly FindYourWayDbContext _context;
        private readonly CompanyService _companyService;

        public MilestonesController(FindYourWayDbContext context)
        {
            _context = context;
            _companyService = new CompanyService(_context);
        }


        // GET: api/Milestones/5
        /// <summary>
        /// Get all milestones of a specific company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Milestone>>> GetCompanyMilestones(int id)
        {
            if (!_companyService.CompanyExists(id)) return BadRequest();
            return await _context.Milestones
                .Where(m=>m.CompanyId==id)
                .ToListAsync();
        }


        // GET: api/Milestones/2/1
        /// <summary>
        /// Get a specific milestone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("milestone/{id}")]
        public async Task<ActionResult<Milestone>> GetMilesone(int id)
        {
            
            var milestone = await _context.Milestones.FindAsync(id);

            if (milestone == null)
            {
                return NotFound();
            }

            return milestone;
        }


        // POST: api/Milestones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Milestone>> PostMilestone(AddMilestoneRequest request)
        {
            var company = await _companyService.GetCompany(request.CompanyId);
            if (company == null) return NotFound();

            if (request.MilestoneName == null) return BadRequest();
            var milestone = new Milestone
            {
                MilestoneName = request.MilestoneName,
                CompanyId = request.CompanyId,
                Company = company
            };

            _context.Milestones.Add(milestone);
            await _context.SaveChangesAsync();

            return Ok(milestone);
        }

        /*
        // PUT: api/Milestones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMilestone(int id, Milestone milestone)
        {
            if (id != milestone.MilestoneId)
            {
                return BadRequest();
            }

            _context.Entry(milestone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilestoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        

        // DELETE: api/Milestones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilestone(int id)
        {
            var milestone = await _context.Milestones.FindAsync(id);
            if (milestone == null)
            {
                return NotFound();
            }

            _context.Milestones.Remove(milestone);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
        private bool MilestoneExists(int id)
        {
            return _context.Milestones.Any(e => e.MilestoneId == id);
        }
        
    }
}
