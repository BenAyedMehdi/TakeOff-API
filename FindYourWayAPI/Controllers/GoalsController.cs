using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Services;
using FindYourWayAPI.Models.DAO;

namespace FindYourWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        public GoalsService GoalsService { get; }

        public GoalsController(GoalsService goalsService)
        {
            GoalsService = goalsService;
        }

        // GET: api/Goals
        /// <summary>
        /// Get all goals of all companies and milestones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goal>>> GetGoals()
        {
            var list = await GoalsService.GetAllGoals();
            return Ok(list);
        }

        // GET: api/Goals/5
        /// <summary>
        /// Get all goals of a milestone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Goal>>> GetMilestonesGoals(int id)
        {
            var goals = await GoalsService.GetMilestoneGoals(id);
            if (goals == null) return BadRequest();
            return Ok(goals);
        }

        // GET: api/Goals/goal/5
        /// <summary>
        /// Get a goal by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("goal/{id}")]
        public async Task<ActionResult<Goal>> GetGoal(int id)
        {
            var goal = await GoalsService.GetGoal(id);
            if (goal == null) return NotFound();
            return Ok(goal);
        }


        // POST: api/Goals
        /// <summary>
        /// Add a goal
        /// </summary>
        /// <param name="goal"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Goal>> PostGoal(AddGoalRequest request)
        {
            var goal = await GoalsService.AddGoal(request);
            if(goal == null) return BadRequest();
            return CreatedAtAction("GetGoal", new { id = goal.GoalId }, goal);
        }


        // PUT: api/Goals/5
        /// <summary>
        /// Uodate a goal (please check UpdateGoalRequest structure)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal(int id, UpdateGoalRequest goal)
        {
            if (!GoalsService.GoalExists(id)) return BadRequest();
            var newGoal = await GoalsService.UpdateGoal(id, goal);
            if (newGoal == null) return BadRequest();
            return Ok(newGoal);
        }


        // DELETE: api/Goals/5
        /// <summary>
        /// Delete goal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            if (!GoalsService.GoalExists(id)) return NotFound();
            await GoalsService.DeleteGoal(id);

            return NoContent();
        }

    }
}
