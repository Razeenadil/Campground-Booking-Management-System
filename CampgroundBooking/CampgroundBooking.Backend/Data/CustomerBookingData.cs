using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.BookingModel;

namespace CampgroundBooking.Backend.Data
{
    public class CustomerBookingData : ICustomerBookingData
    {

        private readonly ISqlDataAcceess _db;

        public CustomerBookingData(ISqlDataAcceess db)
        {
            _db = db;
        }

        public Task<List<CustomerBookingModel>> GetBookings(string userEmail)
        {
            string sql = $"select BookingId, BillingAddress, SiteNo, StartDate, EndDate from dbo.CustomerBooking where CustomerBooking.CustomerEmail = '{userEmail}'";
            return _db.LoadData<CustomerBookingModel, dynamic>(sql, new { });
        }

        public Task DeleteBooking(CustomerBookingModel model)
        {
            string sql = @"delete from dbo.CustomerBooking where BookingId = @BookingId and BillingAddress = @BillingAddress and SiteNo = @SiteNo and StartDate = @StartDate and EndDate = @EndDate";
            return _db.SaveData(sql, model);
        }

        public Task UpdateBooking(CustomerBookingModel model)
        {
            string sql = @"update dbo.CustomerBooking set SiteNo = @SiteNo, StartDate = @StartDate, EndDate = @EndDate where BookingId = @BookingId and BillingAddress = @BillingAddress";
            return _db.SaveData(sql, model);
        }

        public Task<List<AllCustomerBookingModel>> GetAllBookings(string userEmail)
        {
            string sql = $"select StartDate, EndDate, SiteNo from dbo.CustomerBooking where CustomerBooking.CustomerEmail != '{userEmail}'";
            return _db.LoadData<AllCustomerBookingModel, dynamic>(sql, new { });
        }
    }
}
