using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICampgroundServiceData
    {
        Task<List<CampgroundServiceModel>> GetServices(string CGName);
    }
}