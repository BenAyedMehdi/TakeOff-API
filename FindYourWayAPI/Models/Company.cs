namespace FindYourWayAPI.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompnayName { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; } = 0;


        public Field Field { get; set; }
        public Contact? Contact { get; set; }
        public List<Milestone> Milestones { get; set; }
        public List<Product> Products { get; set; }
        public Package Package { get; set; }
    }
}
