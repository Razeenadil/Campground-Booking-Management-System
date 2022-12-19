namespace CampgroundBooking.Backend.Models.CampgroundModel
{
    public class CampgroundReviewModel
    {
        public string? PersonEmail { get; set; }
        public string? Description { get; set; }
        public int StarRating { get; set; } = 0;
    }
}
