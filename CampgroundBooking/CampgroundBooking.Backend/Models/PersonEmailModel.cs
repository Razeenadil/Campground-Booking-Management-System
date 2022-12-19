namespace CampgroundBooking.Backend.Models
{
    public class PersonEmailModel
    {

        public string? Email { get; set; }

        public PersonEmailModel() { }

        public PersonEmailModel(string? email)
        {
            Email = email;
        }
    }
}
