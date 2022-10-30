namespace FindYourWayAPI.Models.DAO
{
    public class UpdateGoalRequest
    {
        public int GoalId { get; set; }
        public string GoalName { get; set; } 
        public bool IsCompleted { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime Deadline { get; set; }

        public int MilestoneId { get; set; }
    }
}
