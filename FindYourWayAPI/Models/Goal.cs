using System.Text.Json.Serialization;

namespace FindYourWayAPI.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string GoalName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime FinishedAt { get; set; }
        public DateTime Deadline { get; set; }

        public int MilestoneId { get; set; }
        [JsonIgnore]
        public Milestone Milestone { get; set; }
    }
}
