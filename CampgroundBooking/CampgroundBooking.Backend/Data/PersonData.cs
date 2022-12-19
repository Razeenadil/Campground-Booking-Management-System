using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models;

namespace CampgroundBooking.Backend.Data
{
    public class PersonData : IPersonData
    {
        private readonly ISqlDataAcceess _db;

        public PersonData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<PersonModel>> GetPeople()
        {
            string sql = "select * from dbo.Person";

            return _db.LoadData<PersonModel, dynamic>(sql, new { });
        }



        public Task InsertPerson(PersonModel model)
        {
            string sql = @"insert into dbo.Person (Email, PhoneNumber, Name, Address)
                            values (@Email, @PhoneNumber, @Name, @Address);";

            return _db.SaveData(sql, model);
        }
    }
}
