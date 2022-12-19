using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICampgroundLocationsData
    {
        Task<List<CampgroundLocationsModel>> GetLocations();
    }
}