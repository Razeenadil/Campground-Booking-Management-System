namespace CampgroundBooking.Backend.Models.EmployeeModel
{
    public class NewEmployeeWorkLocationModel
    {
        public int? ESSN { get; set; }

        public int? CampgroundId { get; set; }

        public string? Email { get; set; }

        public NewEmployeeWorkLocationModel() { }

        public NewEmployeeWorkLocationModel(int? essn, int? id, string? email)
        {
            ESSN = essn;
            CampgroundId = id;
            Email = email;
        }

    }
}
