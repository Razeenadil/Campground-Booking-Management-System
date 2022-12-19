using CampgroundBooking.Backend.Models;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface IPersonData
    {
        Task<List<PersonModel>> GetPeople();
        Task InsertPerson(PersonModel model);
    }
}