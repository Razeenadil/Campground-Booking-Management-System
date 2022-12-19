using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using CampgroundBooking.Backend.Models.CampsiteModel;
using CampgroundBooking.Backend.Models.BookingModel;
using CampgroundBooking.Backend.Models;
using CampgroundBooking.Backend.Models.CampgroundModel;

namespace CampgroundBooking.Backend.CampgroundUtils
{
    public class CampgroundDBServices
    {
        private readonly IConfiguration config;
        public CampgroundDBServices(IConfiguration cfg)
        {
            config = cfg;
        }
        private string dbConnectionString { get { return config.GetConnectionString("Default"); } }

        public async Task<String> GetCampgroundNameFromID(int campgroundID)
        {
            String cgName = "";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT Name FROM [dbo].[Campground] WHERE Id =@campgroundID", con);
            da.SelectCommand.Parameters.AddWithValue("@campgroundID", campgroundID);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                cgName = row["Name"].ToString();
            }
            return await Task.FromResult(cgName);
        }

        public async Task<int> GetCampgroundIDFromName(string campgroundName)
        {
            int cg_ID = -1;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT Id FROM [dbo].[Campground] WHERE Name=@Name", con);
            da.SelectCommand.Parameters.AddWithValue("@Name", campgroundName);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                cg_ID = (int) row["Id"];
            }
            return await Task.FromResult(cg_ID);
        }

        public async Task<List<Campground>> GetAllCampgrounds()
        {
            List<Campground> camprounds = new List<Campground>();

            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = "SELECT Name, Id FROM [dbo].[Campground]";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);
            foreach (DataRow row in dbQuereyResult.Rows)
            {
                string cg_name = (string) row["Name"];
                int cg_id = (int) row["Id"];
                
                Campground currentCG = new Campground(cg_name, cg_id);
                camprounds.Add(currentCG);
            }
            return await Task.FromResult(camprounds);
        }

        public async Task<Campground> GetCampground(int campgroundID)
        {
            Campground campround = null;

            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = "SELECT Name, Id FROM [dbo].[Campground] WHERE Id=@Id";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("Id", campgroundID);
            da.Fill(dbQuereyResult);
            foreach (DataRow row in dbQuereyResult.Rows)
            {
                string cg_name = (string)row["Name"];
                int cg_id = (int)row["Id"];

                campround = new Campground(cg_name, cg_id);
            }
            return await Task.FromResult(campround);
        }

        public async Task<int> getNewestCampgroundID()
        {
            int siteNo = -1;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAX(Id) FROM [dbo].[Campground]", con);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows) { siteNo = ((row["Column1"] == DBNull.Value)) ? 1 : (int)row["Column1"]; }
            return await Task.FromResult(siteNo);
        }

        public async Task<int> InsertCampground(Campground campground)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String insertNewCampgroundSQL = "INSERT INTO [dbo].[Campground] (Name, TotalSites) " +
                                          "VALUES (@Name, @TotalSites);";
            String insertCampgroundLocationSQL = "INSERT INTO [dbo].[CampgroundLocation] (CampgroundId, Lat, Long) " +
                                             "VALUES (@CampgroundId, @Lat, @Long);";
            String insertCampgroundOpenDuringSQL = "INSERT INTO [dbo].[CampgroundOpenDuring] (CampgroundId, OpenDate, CloseDate) " +
                                             "VALUES (@CampgroundId, @OpenDate, @CloseDate);";

            SqlCommand command1 = new SqlCommand(insertNewCampgroundSQL, con);
            SqlCommand command2 = new SqlCommand(insertCampgroundLocationSQL, con);
            SqlCommand command3 = new SqlCommand(insertCampgroundOpenDuringSQL, con);

            command1.Parameters.AddWithValue("@Name", campground.Name);
            command1.Parameters.AddWithValue("@TotalSites", campground.TotalSites);

            con.Open();

            if (command1.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            command2.Parameters.AddWithValue("@CampgroundId", await getNewestCampgroundID());
            command2.Parameters.AddWithValue("@Lat", campground.Latitude);
            command2.Parameters.AddWithValue("@Long", campground.Longitude);

            if (command2.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            command3.Parameters.AddWithValue("@CampgroundId", await getNewestCampgroundID());
            command3.Parameters.AddWithValue("@OpenDate", campground.OpenDate.Value.ToString("yyyy-MM-dd"));
            command3.Parameters.AddWithValue("@CloseDate", campground.CloseDate.Value.ToString("yyyy-MM-dd"));

            return await Task.FromResult(command3.ExecuteNonQuery());
        }

        public async Task<int> DeleteCampground(int campgroundID)
        {
            Console.WriteLine("DELETE CAMPGROUND ID: " + campgroundID);
            SqlConnection con = new SqlConnection(dbConnectionString);
            String DeleteCampsiteSQL = "DELETE FROM [dbo].[Campground] " +
                                       "WHERE Id=@Id";

            SqlCommand command = new SqlCommand(DeleteCampsiteSQL, con);

            con.Open();

            command.Parameters.AddWithValue("@Id", campgroundID);

            return await Task.FromResult(command.ExecuteNonQuery());
        }

      
    }
}
