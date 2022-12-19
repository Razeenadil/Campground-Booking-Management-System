namespace CampgroundBooking.Backend.Models.CampgroundModel
{
    public class CampgroundStoreModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
    }
}
