using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICampgroundSitesData
    {
        Task<List<CampgroundRVSitesModel>> GetRVSites(string CGName);
        Task<List<CampgroundTentSitesModel>> GetTentSites(string CGName);
    }
}