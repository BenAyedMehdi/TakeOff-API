using System.Text.Json.Serialization;

namespace FindYourWayAPI.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string ReportTitle { get; set; }
        public string Content { get; set; }
        public DateTime IssuedAt { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }

    }
}
