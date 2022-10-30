namespace FindYourWayAPI.Models.DAO
{
    public class AddProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;

        public int CompanyId { get; set; } = 0;
    }
}
