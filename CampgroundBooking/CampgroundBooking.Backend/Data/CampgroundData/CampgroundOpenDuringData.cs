using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Data.CampgroundData
{
    public class CampgroundOpenDuringData : ICampgroundOpenDuringData
    {
        private readonly ISqlDataAcceess _db;

        public CampgroundOpenDuringData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<CampgroundOpenDuringModel>> GetOpenDuring(string CGName)
        {
            string sql = $"select OpenDate, CloseDate from dbo.CampgroundOpenDuring, dbo.Campground where" +
                $" CampgroundOpenDuring.CampgroundId = Campground.Id and Campground.Name = '{CGName}'";
            return _db.LoadData<CampgroundOpenDuringModel, dynamic>(sql, new { });

        }
    }
}
