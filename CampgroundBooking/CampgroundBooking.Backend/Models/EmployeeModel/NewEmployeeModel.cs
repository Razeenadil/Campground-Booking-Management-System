namespace CampgroundBooking.Backend.Models.EmployeeModel
{
    public class NewEmployeeModel
    {
        public string? Email { get; set; }

        public int? ESSN { get; set; }

        public int? Age { get; set; }

        public bool? Sex { get; set; }

        public NewEmployeeModel() { }

        public NewEmployeeModel(string? email, int? essn, int? age, bool? sex)
        {
            ESSN = essn;
            Email = email;
            Age = age;
            Sex = sex;
        }
    }
}
