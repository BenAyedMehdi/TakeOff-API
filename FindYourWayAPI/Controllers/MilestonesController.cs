using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using FindYourWayAPI.Services;
using FindYourWayAPI.Data;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestonesController : ControllerBase
    {
        private readonly MilestoneService milestoneService;

        public MilestonesController( MilestoneService milestoneService)
        {
            this.milestoneService = milestoneService;
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
            var list = await milestoneService.GetCompanyMilestones(id);
            return Ok(list);
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

            var milestone = await milestoneService.GetMilesone(id);
            if (milestone == null)
            {
                return NotFound();
            }
            return milestone;
        }


        // POST: api/Milestones
        /// <summary>
        /// Add a new milestone
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Milestone>> PostMilestone(AddMilestoneRequest request)
        {
            var milestone = await milestoneService.AddMilestone(request);
            if (milestone == null) return BadRequest();
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

        
        */
        // DELETE: api/Milestones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilestone(int id)
        {
            if (!milestoneService.MilestoneExists(id)) return NotFound();
            await milestoneService.DeleteMilestone(id);
            return NoContent();
        }
        
        

    }
}
