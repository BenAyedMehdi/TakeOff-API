namespace FindYourWayAPI.Models.DAO
{
    public class AddGoalRequest
    {
        public string GoalName { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }

        public int MilestoneId { get; set; }
    }
}
