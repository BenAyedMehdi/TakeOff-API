using FindYourWayAPI.Data;
using FindYourWayAPI.Models;
using FindYourWayAPI.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWayAPI.Services
{
    public class GoalsService
    {
        private readonly FindYourWayDbContext _context;
        private readonly MilestoneService milestoneService;

        public GoalsService(FindYourWayDbContext context, MilestoneService milestoneService)
        {
            _context = context;
            this.milestoneService = milestoneService;
        }

        public async Task<IEnumerable<Goal>> GetAllGoals()
        {
            var list= await _context.Goals.ToListAsync();
            return list;
        }
        public async Task<IEnumerable<Goal>> GetMilestoneGoals(int id)
        {
            var milestone = await milestoneService.GetMilesone(id);
            if (milestone == null) return null;

            var list = await _context.Goals.Where(g=>g.MilestoneId == id).ToListAsync();
            return list;
        }
        public async Task<Goal> GetGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            return goal;
        }
        public async Task<Goal> AddGoal(AddGoalRequest request)
        {
            var milestone = await milestoneService.GetMilesone(request.MilestoneId);
            if (milestone == null) return null;
            var goal = new Goal
            {
                GoalName = request.GoalName,
                Deadline = request.Deadline,
                CreatedAt = DateTime.Now,
                IsCompleted = false,
                MilestoneId = request.MilestoneId,
                Milestone = milestone
            };
            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return goal;
        }
        public async Task<Goal> UpdateGoal(int id, UpdateGoalRequest request)
        {
            var goal = await GetGoal(id);

            if (id != goal.GoalId)
            {
                return null;
            }
            goal.GoalName = request.GoalName;
            goal.Deadline = request.Deadline;
            goal.FinishedAt = request.FinishedAt;
            goal.IsCompleted = request.IsCompleted;
            _context.Entry(goal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task DeleteGoal(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

        }
        public bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.GoalId == id);
        }
    }
}
