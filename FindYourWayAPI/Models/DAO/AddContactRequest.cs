namespace FindYourWayAPI.Models.DAO
{
    public class AddContactRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        public int OwnerId { get; set; } = 0;
    }
}
