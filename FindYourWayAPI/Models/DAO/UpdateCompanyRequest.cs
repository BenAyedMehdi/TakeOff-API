namespace FindYourWayAPI.Models.DAO
{
    public class UpdateCompanyRequest
    {
        public int CompanyId { get; set; }
        public string CompnayName { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; } = 0;


        public int FieldId { get; set; }
        public int PackageId { get; set; }
    }
}
