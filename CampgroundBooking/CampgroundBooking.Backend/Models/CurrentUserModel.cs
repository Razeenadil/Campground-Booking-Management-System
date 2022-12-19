namespace CampgroundBooking.Backend.Models
{
    public static class CurrentUserModel
    {
        private static string? UserEmail;

        private static readonly string? admin = "admin@campground.com";

        public static void SetUserEmail(string user)
        {
            UserEmail = user;
        }


        public static string? GetUserEmail()
        {
            return UserEmail;
        }

        public static string? GetAdminUser()
        {
            return admin;
        }

    }
}
