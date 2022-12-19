using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampgroundBooking.Backend.Models.EmployeeModel
{
    public class NewEmployeeTaskModel
    {
        public int? ESSN { get; set; }

        public string? Description { get; set; }

        public string? Email { get; set; }



        public NewEmployeeTaskModel() { }

        public NewEmployeeTaskModel(int? essn, string? description, string? email)
        {
            ESSN = essn;
            Description = description;
            Email = email;
        }
    }
}
