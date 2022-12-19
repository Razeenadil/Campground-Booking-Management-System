using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.CampgroundModel;
using CampgroundBooking.Backend.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace CampgroundBooking.Backend.Data.CampgroundData
{
    public class CampgroundReviewData : ICampgroundReviewData
    {
        private readonly ISqlDataAcceess _db;
        private string dbConnectionString { get { return config.GetConnectionString("Default"); } }

        private readonly IConfiguration config;
        public CampgroundReviewData(IConfiguration cfg, ISqlDataAcceess db)
        {
            config = cfg;
            _db = db;
        }

        public Task<List<CampgroundReviewModel>> GetReviews(string CGName)
        {
            string sql = $"select CampgroundReview.PersonEmail, CampgroundReview.Description, CampgroundReview.StarRating from dbo.CampgroundReview, dbo.Campground where" +
                $" Campground.Name = '{CGName}' and Campground.Id = CampgroundReview.CampgroundId";
            return _db.LoadData<CampgroundReviewModel, dynamic>(sql, new { });
        }

        public Task<List<PersonReviewModel>> GetPersonReviews(string userEmail)
        {
            string sql = $"select Campground.Name, CampgroundReview.Description, CampgroundReview.StarRating from dbo.CampgroundReview, dbo.Campground where" +
                $" CampgroundReview.PersonEmail = '{userEmail}' and CampgroundReview.CampgroundId = Campground.Id";
            return _db.LoadData<PersonReviewModel, dynamic>(sql, new { });
        }
        //INSERT INTO REVIEW VALUES (CG_ID, Person_email, Review_Description, Star_Rating)
        public async Task<int> insertReview(int CGID, string email, string desc, int rating)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            string sql = "INSERT INTO [dbo].[CampgroundReview] (" +
                            "CampgroundId, PersonEmail, Description, StarRating) " +
                         "VALUES (" +
                            "@CampgroundId, " +
                            "@PersonEmail, " +
                            "@Description, " +
                            "@StarRating);";

            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@CampgroundId", CGID);
            command.Parameters.AddWithValue("@PersonEmail", email);
            command.Parameters.AddWithValue("@Description", desc);
            command.Parameters.AddWithValue("@StarRating", rating);

            con.Open();
            return await Task.FromResult(command.ExecuteNonQuery());
        }

    }
}
