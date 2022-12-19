namespace CampgroundBooking.Backend.Models.EmployeeModel
{
    public class EmployeeSSNsModel
    {
        public int? ESSN { get; set; }

        public EmployeeSSNsModel(int? eSSN)
        {
            ESSN = eSSN;
        }
    }
}
