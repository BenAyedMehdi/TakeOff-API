namespace FindYourWayAPI.Models.DAO
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public int CompanyId { get; set; } = 0;
    }
}
