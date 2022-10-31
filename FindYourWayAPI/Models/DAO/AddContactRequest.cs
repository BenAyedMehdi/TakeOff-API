namespace FindYourWayAPI.Models.DAO
{
    public class AddContactRequest
    {
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Adress { get; set; } 
        public string Website { get; set; } 

        public int OwnerId { get; set; }
    }
}
