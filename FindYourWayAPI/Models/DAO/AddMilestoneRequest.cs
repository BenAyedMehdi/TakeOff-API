
namespace FindYourWayAPI.Models.DAO
{
    public class AddMilestoneRequest
    {
        public string MilestoneName { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
    }
}
