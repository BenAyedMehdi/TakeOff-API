namespace FindYourWayAPI.Models.DAO
{
    public class AddReportRequest
    {
        public string ReportTitle { get; set; }
        public string Content { get; set; }
        public int CompanyId { get; set; }
    }
}
