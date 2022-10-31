namespace FindYourWayAPI.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;


    }
}
