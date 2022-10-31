using System.Text.Json.Serialization;

namespace FindYourWayAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;   
        public Category Category { get; set; }

        public int CompanyId { get; set; } = 0;
        [JsonIgnore]
        public Company Company { get; set; }
    }
}
