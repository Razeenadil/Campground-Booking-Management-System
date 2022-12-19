using CampgroundBooking.Backend.Models.BookingModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface ICustomerBookingData
    {
        Task<List<CustomerBookingModel>> GetBookings(string userEmail);

        Task DeleteBooking(CustomerBookingModel model);

        Task UpdateBooking(CustomerBookingModel model);

        Task<List<AllCustomerBookingModel>> GetAllBookings(string userEmail);



    }
}