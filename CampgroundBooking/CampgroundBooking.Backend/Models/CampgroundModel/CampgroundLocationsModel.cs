namespace CampgroundBooking.Backend.Models.CampgroundModel
{
    public class CampgroundLocationsModel
    {
        public int CampgroundId { get; set; }

        public string? Name { get; set; }

        public float Lat { get; set; }

        public float Long { get; set; }
    }
}
