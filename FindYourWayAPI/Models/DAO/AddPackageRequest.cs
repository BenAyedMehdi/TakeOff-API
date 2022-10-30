namespace FindYourWayAPI.Models.DAO
{
    public class AddPackageRequest
    {
        public string PackageName { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }
}
