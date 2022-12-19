using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.Data.CampgroundData
{
    public class CampgroundSitesData : ICampgroundSitesData
    {
        private readonly ISqlDataAcceess _db;

        public CampgroundSitesData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<CampgroundTentSitesModel>> GetTentSites(string CGName)
        {
            string sql = $"select count(*) as TotalTentSites from dbo.CampsiteTenting, dbo.Campsite, dbo.Campground where Campsite.SiteNo = CampsiteTenting.SiteNo and Campsite.CampgroundId = Campground.Id and Campground.Name = '{CGName}'";
            return _db.LoadData<CampgroundTentSitesModel, dynamic>(sql, new { });
        }

        public Task<List<CampgroundRVSitesModel>> GetRVSites(string CGName)
        {
            string sql = $"select count(*) as TotalRVSites from dbo.CampsiteRV, dbo.Campsite, dbo.Campground where Campsite.SiteNo = CampsiteRV.SiteNo and Campsite.CampgroundId = Campground.Id and Campground.Name = '{CGName}'";
            return _db.LoadData<CampgroundRVSitesModel, dynamic>(sql, new { });
        }
    }
}
