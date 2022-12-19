using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Data.CampgroundData
{
    public class CampgroundAmenitiesData : ICampgroundAmenitiesData
    {
        private readonly ISqlDataAcceess _db;

        public CampgroundAmenitiesData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<CampgroundHikingModel>> GetHiking(string CGName)
        {
            string sql = $"select AmenityHiking.Name, Amenity.Description, AmenityHiking.Experience, AmenityHiking.Elevation from dbo.AmenityHiking, dbo.Amenity, dbo.Campground where" +
                $" Campground.Name = '{CGName}' and Campground.Id = Amenity.CampgroundId and AmenityHiking.Name = Amenity.Name";
            return _db.LoadData<CampgroundHikingModel, dynamic>(sql, new { });
        }

        public Task<List<CampgroundBikingModel>> GetBiking(string CGName)
        {
            string sql = $"select AmenityBike.Name, Amenity.Description, AmenityBike.Experience, AmenityBike.Distance from dbo.AmenityBike, dbo.Amenity, dbo.Campground where" +
                $" Campground.Name = '{CGName}' and Campground.Id = Amenity.CampgroundId and AmenityBike.Name = Amenity.Name";
            return _db.LoadData<CampgroundBikingModel, dynamic>(sql, new { });
        }

        public Task<List<CampgroundLakeModel>> GetLake(string CGName)
        {
            string sql = $"select AmenityLake.Name, Amenity.Description, AmenityLake.Depth, AmenityLake.Area, AmenityLake.BoatingAllowed, AmenityLake.FishingAllowed from dbo.AmenityLake, dbo.Amenity, dbo.Campground where" +
                $" Campground.Name = '{CGName}' and Campground.Id = Amenity.CampgroundId and AmenityLake.Name = Amenity.Name";
            return _db.LoadData<CampgroundLakeModel, dynamic>(sql, new { });
        }

        public Task<List<CampgroundStoreModel>> GetStore(string CGName)
        {
            string sql = $"select AmenityStore.Name, Amenity.Description, AmenityStore.ClosingHour, AmenityStore.OpeningHour from dbo.AmenityStore, dbo.Amenity, dbo.Campground where" +
                $" Campground.Name = '{CGName}' and Campground.Id = Amenity.CampgroundId and AmenityStore.Name = Amenity.Name";
            return _db.LoadData<CampgroundStoreModel, dynamic>(sql, new { });
        }

        public Task<List<CampgroundStoreProductModel>> GetStoreProducts(string storeName)
        {
            string sql = $"select AmenityStoreProducts.Product from dbo.AmenityStore, dbo.AmenityStoreProducts where" +
                $" AmenityStore.Name = '{storeName}' and AmenityStore.Name = AmenityStoreProducts.Name";
            return _db.LoadData<CampgroundStoreProductModel, dynamic>(sql, new { });
        }



    }
}
