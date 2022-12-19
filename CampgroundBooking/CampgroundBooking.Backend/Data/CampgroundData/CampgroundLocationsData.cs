using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Data.CampgroundData
{
    public class CampgroundLocationsData : ICampgroundLocationsData
    {
        private readonly ISqlDataAcceess _db;

        public CampgroundLocationsData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<CampgroundLocationsModel>> GetLocations()
        {
            string sql = "select Name, CampgroundId, Lat, Long from dbo.Campground, dbo.CampgroundLocation where Id = CampgroundId";
            return _db.LoadData<CampgroundLocationsModel, dynamic>(sql, new { });
        }

    }
}
