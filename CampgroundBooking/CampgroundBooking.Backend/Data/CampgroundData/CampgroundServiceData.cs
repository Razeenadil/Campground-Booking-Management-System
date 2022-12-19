using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Data.CampgroundData
{
    public class CampgroundServiceData : ICampgroundServiceData
    {

        private readonly ISqlDataAcceess _db;

        public CampgroundServiceData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<CampgroundServiceModel>> GetServices(string CGName)
        {
            string sql = $"select SiteService from dbo.CampgroundServices, dbo.Campground where CampgroundServices.CampgroundId = Campground.Id" +
                $" and Campground.Name = '{CGName}'";
            return _db.LoadData<CampgroundServiceModel, dynamic>(sql, new { });

        }
    }
}
