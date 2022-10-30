using System.Text.Json.Serialization;

namespace FindYourWayAPI.Models
{
    public class Milestone
    {
        public int MilestoneId { get; set; }
        public string MilestoneName { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
    }
}
