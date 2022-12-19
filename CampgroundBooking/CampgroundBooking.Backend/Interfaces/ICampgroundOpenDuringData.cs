using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICampgroundOpenDuringData
    {
        Task<List<CampgroundOpenDuringModel>> GetOpenDuring(string CGName);
    }
}