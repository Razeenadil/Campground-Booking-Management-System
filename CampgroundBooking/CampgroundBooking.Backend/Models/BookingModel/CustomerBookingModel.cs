namespace CampgroundBooking.Backend.Models.BookingModel
{
    public class CustomerBookingModel
    {
        public int BookingId { get; set; }

        public string? BillingAddress { get; set; }

        public int SiteNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
