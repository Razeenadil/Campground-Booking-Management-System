using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICampgroundAmenitiesData
    {
        Task<List<CampgroundHikingModel>> GetHiking(string CGName);

        Task<List<CampgroundBikingModel>> GetBiking(string CGName);

        Task<List<CampgroundLakeModel>> GetLake(string CGName);

        Task<List<CampgroundStoreModel>> GetStore(string CGName);

        Task<List<CampgroundStoreProductModel>> GetStoreProducts(string storeName);
    }
}