using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using CampgroundBooking.Backend.Models.CampsiteModel;
using CampgroundBooking.Backend.Models.BookingModel;
using CampgroundBooking.Backend.Models.EmployeeModel;

namespace CampgroundBooking.Backend.CampsiteUtils
{
    public class CampsiteDBServices
    {
        private readonly IConfiguration config;
        public CampsiteDBServices(IConfiguration cfg)
        {
            config = cfg;
        }
        private string dbConnectionString { get { return config.GetConnectionString("Default"); } }

        public async Task<String> getCampgroundNameFromID(int campgroundID)
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
        public async Task<List<Campsite>> getCampsiteList(int campgroundID)
        {
            List<Campsite> campsites = new List<Campsite>();

            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = "SELECT C.SiteNo, C.CampgroundId, RV.PowerHookUp, RV.WasteDump, RV.WaterHookUp, RV.MaxRVSize, T.TentsPerSite " +
                         "FROM (([dbo].[Campsite] AS C FULL OUTER JOIN [dbo].[CampsiteTenting] AS T ON C.SiteNo = T.SiteNo) " +
                                "FULL OUTER JOIN [dbo].[CampsiteRV] AS RV ON RV.SiteNo = C.SiteNo)" +
                         "WHERE C.CampgroundId = " + campgroundID + ";";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);
            foreach (DataRow row in dbQuereyResult.Rows)
            {
                //See if row is a tent site or not
                if (String.IsNullOrEmpty(row["MaxRVSize"].ToString()))
                {
                    //No Max RV size entry exists but tents per site entry exists, site is a exclusive tenting site
                    int siteNo = (int)row["SiteNo"];
                    int cg_id = (int)row["CampgroundId"];
                    string cg_name = getCampgroundNameFromID(campgroundID).Result;
                    int maxTentsPerSite = (int)row["TentsPerSite"];

                    Campsite currentSite = Campsite.TentingSite(siteNo, cg_id, cg_name, maxTentsPerSite);
                    campsites.Add(currentSite);
                }
                else if (String.IsNullOrEmpty(row["TentsPerSite"].ToString()))
                {
                    //Max RV size entry exists and no tents per site entry, site is an exclusive RV site
                    int siteNo = (int)row["SiteNo"];
                    int cg_id = (int)row["CampgroundId"];
                    string cg_name = getCampgroundNameFromID(campgroundID).Result;
                    bool hasPower = (bool)row["PowerHookUp"];
                    bool hasWater = (bool)row["WaterHookUp"];
                    bool hasWasteDump = (bool)row["WasteDump"];
                    int maxRVSize = (int)row["MaxRVSize"];

                    Campsite currentSite = Campsite.RVSite(siteNo, cg_id, cg_name, hasPower, hasWater, hasWasteDump, maxRVSize);
                    campsites.Add(currentSite);
                }
                else
                {
                    //Max RV size entry AND tents per site entry exists, site is an mixed campsite
                    int siteNo = (int)row["SiteNo"];
                    int cg_id = (int)row["CampgroundId"];
                    string cg_name = getCampgroundNameFromID(campgroundID).Result;
                    bool hasPower = (bool)row["PowerHookUp"];
                    bool hasWater = (bool)row["WaterHookUp"];
                    bool hasWasteDump = (bool)row["WasteDump"];
                    int maxRVSize = (int)row["MaxRVSize"];
                    int maxTentsPerSite = (int)row["TentsPerSite"];

                    Campsite currentSite = Campsite.MixedSite(siteNo, cg_id, cg_name, hasPower, hasWater, hasWasteDump, maxRVSize, maxTentsPerSite);
                    campsites.Add(currentSite);
                }
            }
            return await Task.FromResult(campsites);
        }

        public async Task<Campsite> getCampsite(int campgroundID, int campsiteNo)
        {
            Campsite? campsite = null;

            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = "SELECT C.SiteNo, C.CampgroundId, RV.PowerHookUp, RV.WasteDump, RV.WaterHookUp, RV.MaxRVSize, T.TentsPerSite " +
                         "FROM (([dbo].[Campsite] AS C FULL OUTER JOIN [dbo].[CampsiteTenting] AS T ON C.SiteNo = T.SiteNo) " +
                                "FULL OUTER JOIN [dbo].[CampsiteRV] AS RV ON RV.SiteNo = C.SiteNo)" +
                         "WHERE C.CampgroundId = " + campgroundID + " AND C.SiteNo = " + campsiteNo + ";";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);
            foreach (DataRow row in dbQuereyResult.Rows)
            {
                //See if row is a tent site or not
                if (String.IsNullOrEmpty(row["MaxRVSize"].ToString()))
                {
                    //No Max RV size entry exists but tents per site entry exists, site is a exclusive tenting site
                    int siteNo = (int)row["SiteNo"];
                    int cg_id = (int)row["CampgroundId"];
                    string cg_name = getCampgroundNameFromID(campgroundID).Result;
                    int maxTentsPerSite = (int)row["TentsPerSite"];

                    Campsite currentSite = Campsite.TentingSite(siteNo, cg_id, cg_name, maxTentsPerSite);
                    campsite = currentSite;
                }
                else if (String.IsNullOrEmpty(row["TentsPerSite"].ToString()))
                {
                    //Max RV size entry exists and no tents per site entry, site is an exclusive RV site
                    int siteNo = (int)row["SiteNo"];
                    int cg_id = (int)row["CampgroundId"];
                    string cg_name = getCampgroundNameFromID(campgroundID).Result;
                    bool hasPower = (bool)row["PowerHookUp"];
                    bool hasWater = (bool)row["WaterHookUp"];
                    bool hasWasteDump = (bool)row["WasteDump"];
                    int maxRVSize = (int)row["MaxRVSize"];

                    Campsite currentSite = Campsite.RVSite(siteNo, cg_id, cg_name, hasPower, hasWater, hasWasteDump, maxRVSize);
                    campsite = currentSite;
                }
                else
                {
                    //Max RV size entry AND tents per site entry exists, site is an mixed campsite
                    int siteNo = (int)row["SiteNo"];
                    int cg_id = (int)row["CampgroundId"];
                    string cg_name = getCampgroundNameFromID(campgroundID).Result;
                    bool hasPower = (bool)row["PowerHookUp"];
                    bool hasWater = (bool)row["WaterHookUp"];
                    bool hasWasteDump = (bool)row["WasteDump"];
                    int maxRVSize = (int)row["MaxRVSize"];
                    int maxTentsPerSite = (int)row["TentsPerSite"];

                    Campsite currentSite = Campsite.MixedSite(siteNo, cg_id, cg_name, hasPower, hasWater, hasWasteDump, maxRVSize, maxTentsPerSite);
                    campsite = currentSite;
                }
            }
            return await Task.FromResult(campsite);
        }

        public async Task<List<CustomerBookingModel>> getCampsiteBookings(int campgroundID, int campsiteNo)
        {
            List<CustomerBookingModel>? bookings = new List<CustomerBookingModel>();

            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = "SELECT B.SiteNo, B.BookingId, B.BillingAddress, B.StartDate, B.EndDate, B.CustomerEmail " +
                         "FROM ([dbo].[Campsite] AS C JOIN [dbo].[CustomerBooking] AS B ON C.SiteNo = B.SiteNo) " +
                         "WHERE C.CampgroundId = " + campgroundID + " AND C.SiteNo = " + campsiteNo + ";";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);
            foreach (DataRow row in dbQuereyResult.Rows)
            {
                int bookingId = (int) row["BookingId"];
                string billingAddress = (string) row["BillingAddress"];
                DateTime startDate = (DateTime)row["StartDate"];
                DateTime endDate = (DateTime)row["EndDate"];
                int siteNo = (int)row["SiteNo"];

                CustomerBookingModel currentBooking = new()
                {
                    BookingId = bookingId,
                    BillingAddress = billingAddress,
                    SiteNo = siteNo,
                    StartDate = startDate,
                    EndDate = endDate
                };
                bookings.Add(currentBooking);
            }
            return await Task.FromResult(bookings);
        }

        public async Task<int> getNewestCampsiteNo()
        {
            int siteNo = -1;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAX(SiteNo) FROM [dbo].[Campsite]", con);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows) { siteNo = ((row["Column1"] == DBNull.Value)) ? 1000 : (int)row["Column1"]; }
            return await Task.FromResult(siteNo);
        }

        public async Task<int> getNewestCampsiteNoForCampground(int campgroundId)
        {
            int siteNo = -1;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAX(SiteNo) FROM [dbo].[Campsite] WHERE CampgroundId=@CampgroundId", con);
            da.SelectCommand.Parameters.AddWithValue("@CampgroundId", campgroundId);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows) { siteNo = ((row["Column1"] == DBNull.Value)) ? 1000 : (int)row["Column1"]; }
            return await Task.FromResult(siteNo);
        }
        public async Task<int> InsertCampsite(Campsite campsite)
        {

            if (campsite.siteType.Equals(SiteType.Tenting)) { return await InsertTentingCampsite(campsite); }
            else if (campsite.siteType.Equals(SiteType.RV)) { return await InsertRVCampsite(campsite); }
            else { return await InsertMixedCampsite(campsite); }
        }

        public async Task<int> InsertTentingCampsite(Campsite campsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String insertNewCampsiteSQL = "INSERT INTO [dbo].[Campsite] (CampgroundId) " +
                                          "VALUES (@CampgroundId);";
            String insertNewTentingCampsiteSQL = "INSERT INTO [dbo].[CampsiteTenting] (SiteNo, TentsPerSite)" +
                                             "VALUES (@SiteNo, @TentsPerSite);";
            SqlCommand command1 = new SqlCommand(insertNewCampsiteSQL, con);
            SqlCommand command2 = new SqlCommand(insertNewTentingCampsiteSQL, con);

            command1.Parameters.AddWithValue("@CampgroundId", campsite.campgroundID);

            con.Open();

            if (command1.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            command2.Parameters.AddWithValue("@SiteNo", await getNewestCampsiteNo());
            command2.Parameters.AddWithValue("@TentsPerSite", campsite.maxTentsPerSite);

            return await Task.FromResult(command2.ExecuteNonQuery());
        }

        public async Task<int> InsertRVCampsite(Campsite campsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String insertNewCampsiteSQL = "INSERT INTO [dbo].[Campsite] (CampgroundId) " +
                                          "VALUES (@CampgroundId);";
            String insertNewRVCampsiteSQL = "INSERT INTO [dbo].[CampsiteRV] (SiteNo, " +
                                                         "PowerHookUp, " +
                                                         "WasteDump, " +
                                                         "WaterHookUp, " +
                                                         "MaxRVSize)" +
                                             "VALUES (@SiteNo, " +
                                                     "@PowerHookUp, " +
                                                     "@WasteDump, " +
                                                     "@WaterHookUp, " +
                                                     "@MaxRVSize);";
            SqlCommand command1 = new SqlCommand(insertNewCampsiteSQL, con);
            SqlCommand command2 = new SqlCommand(insertNewRVCampsiteSQL, con);
            
            command1.Parameters.AddWithValue("@CampgroundId", campsite.campgroundID);

            con.Open();

            if (command1.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }
            
            command2.Parameters.AddWithValue("@SiteNo", await getNewestCampsiteNo());
            command2.Parameters.AddWithValue("@PowerHookUp", campsite.hasPower);
            command2.Parameters.AddWithValue("@WasteDump", campsite.hasWasteDump);
            command2.Parameters.AddWithValue("@WaterHookUp", campsite.hasWater);
            command2.Parameters.AddWithValue("@MaxRVSize", campsite.maxRVSize);

            return await Task.FromResult(command2.ExecuteNonQuery());
        }

        public async Task<int> InsertMixedCampsite(Campsite campsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String insertNewCampsiteSQL = "INSERT INTO [dbo].[Campsite] (CampgroundId) " +
                                          "VALUES (@CampgroundId);";
            String insertNewRVCampsiteSQL = "INSERT INTO [dbo].[CampsiteRV] (SiteNo, " +
                                                         "PowerHookUp, " +
                                                         "WasteDump, " +
                                                         "WaterHookUp, " +
                                                         "MaxRVSize)" +
                                             "VALUES (@SiteNo, " +
                                                     "@PowerHookUp, " +
                                                     "@WasteDump, " +
                                                     "@WaterHookUp, " +
                                                     "@MaxRVSize);";
            String insertNewTentingCampsiteSQL = "INSERT INTO [dbo].[CampsiteTenting] (SiteNo, TentsPerSite)" +
                                                 "VALUES (@SiteNo, @TentsPerSite);";
            SqlCommand command1 = new SqlCommand(insertNewCampsiteSQL, con);
            SqlCommand command2 = new SqlCommand(insertNewRVCampsiteSQL, con);
            SqlCommand command3 = new SqlCommand(insertNewTentingCampsiteSQL, con);

            command1.Parameters.AddWithValue("@CampgroundId", campsite.campgroundID);

            con.Open();

            if (command1.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            command2.Parameters.AddWithValue("@SiteNo", await getNewestCampsiteNo());
            command2.Parameters.AddWithValue("@PowerHookUp", campsite.hasPower);
            command2.Parameters.AddWithValue("@WasteDump", campsite.hasWasteDump);
            command2.Parameters.AddWithValue("@WaterHookUp", campsite.hasWater);
            command2.Parameters.AddWithValue("@MaxRVSize", campsite.maxRVSize);

            command3.Parameters.AddWithValue("@SiteNo", await getNewestCampsiteNo());
            command3.Parameters.AddWithValue("@TentsPerSite", campsite.maxTentsPerSite);

            if (command2.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            return await Task.FromResult(command3.ExecuteNonQuery());
        }

        public async Task<int> UpdateCampsite(Campsite updatedCampsite)
        {

            if (updatedCampsite.siteType.Equals(SiteType.Tenting)) { return await UpdateTentingCampsite(updatedCampsite); }
            else if (updatedCampsite.siteType.Equals(SiteType.RV)) { return await UpdateRVCampsite(updatedCampsite); }
            else { return await UpdateMixedCampsite(updatedCampsite); }
        }

        public async Task<int> UpdateTentingCampsite(Campsite updatedCampsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String UpdateTentingCampsiteSQL = "UPDATE [dbo].[CampsiteTenting] " +
                                                 "SET TentsPerSite=@TentsPerSite " +
                                                 "WHERE SiteNo=@SiteNo";

            SqlCommand command = new SqlCommand(UpdateTentingCampsiteSQL, con);

            con.Open();

            command.Parameters.AddWithValue("@SiteNo", updatedCampsite.campsiteNum);
            command.Parameters.AddWithValue("@TentsPerSite", updatedCampsite.maxTentsPerSite);

            return await Task.FromResult(command.ExecuteNonQuery());
        }

        public async Task<int> UpdateRVCampsite(Campsite updatedCampsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String UpdateRVCampsiteSQL = "UPDATE [dbo].[CampsiteRV] " +
                                         "SET PowerHookUp=@PowerHookUp, " +
                                             "WasteDump=@WasteDump, " +
                                             "WaterHookUp=@WaterHookUp, " +
                                             "MaxRVSize=@MaxRVSize " +
                                          "WHERE SiteNo=@SiteNo";

            SqlCommand command = new SqlCommand(UpdateRVCampsiteSQL, con);

            con.Open();
            command.Parameters.AddWithValue("@SiteNo", updatedCampsite.campsiteNum);
            command.Parameters.AddWithValue("@PowerHookUp", updatedCampsite.hasPower);
            command.Parameters.AddWithValue("@WasteDump", updatedCampsite.hasWasteDump);
            command.Parameters.AddWithValue("@WaterHookUp", updatedCampsite.hasWater);
            command.Parameters.AddWithValue("@MaxRVSize", updatedCampsite.maxRVSize);
            
            return await Task.FromResult(command.ExecuteNonQuery());
        }

        public async Task<int> UpdateMixedCampsite(Campsite updatedCampsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String UpdateTentingCampsiteSQL = "UPDATE [dbo].[CampsiteTenting] " +
                                                 "SET TentsPerSite=@TentsPerSite " +
                                                 "WHERE SiteNo=@SiteNo";
            
            String UpdateRVCampsiteSQL = "UPDATE [dbo].[CampsiteRV] " +
                                         "Set PowerHookUp=@PowerHookUp, " +
                                             "WasteDump=@WasteDump, " +
                                             "WaterHookUp=@WaterHookUp, " +
                                             "MaxRVSize=@MaxRVSize " +
                                         "WHERE SiteNo=@SiteNo;";

            SqlCommand command1 = new SqlCommand(UpdateTentingCampsiteSQL, con);
            SqlCommand command2 = new SqlCommand(UpdateRVCampsiteSQL, con);

            con.Open();

            command1.Parameters.AddWithValue("@SiteNo", updatedCampsite.campsiteNum);
            command1.Parameters.AddWithValue("@TentsPerSite", updatedCampsite.maxTentsPerSite);


            command2.Parameters.AddWithValue("@SiteNo", updatedCampsite.campsiteNum);
            command2.Parameters.AddWithValue("@PowerHookUp", updatedCampsite.hasPower);
            command2.Parameters.AddWithValue("@WasteDump", updatedCampsite.hasWasteDump);
            command2.Parameters.AddWithValue("@WaterHookUp", updatedCampsite.hasWater);
            command2.Parameters.AddWithValue("@MaxRVSize", updatedCampsite.maxRVSize);

            if (command1.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            return await Task.FromResult(command2.ExecuteNonQuery());
        }

        public async Task<int> DeleteCampsite(int siteNo, int campgroundId)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String DeleteCampsiteSQL = "DELETE FROM [dbo].[Campsite] " +
                                       "WHERE SiteNo=@SiteNo AND CampgroundId=@CampgroundId";

            SqlCommand command = new SqlCommand(DeleteCampsiteSQL, con);

            con.Open();

            command.Parameters.AddWithValue("@SiteNo", siteNo);
            command.Parameters.AddWithValue("@CampgroundId", campgroundId);

            return await Task.FromResult(command.ExecuteNonQuery());
        }

        public async Task<int> DeleteCampsite(Campsite campsite)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String DeleteCampsiteSQL = "DELETE FROM [dbo].[Campsite] " +
                                       "WHERE SiteNo=@SiteNo AND CampgroundId=@CampgroundId";

            SqlCommand command = new SqlCommand(DeleteCampsiteSQL, con);

            con.Open();

            command.Parameters.AddWithValue("@SiteNo", campsite.campsiteNum);
            command.Parameters.AddWithValue("@CampgroundId", campsite.campgroundID);

            return await Task.FromResult(command.ExecuteNonQuery());
        }

        public async Task<String> getCustomerAddressFromEmail(string email)
        {
            String addr = "";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlCommand command = new SqlCommand("SELECT Address FROM [dbo].[Person] WHERE Email=@Email", con);
            command.Parameters.AddWithValue("@Email", email);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            return await Task.FromResult((String) reader[0]);
        }

        public async Task<int> insertCampsiteBooking(string email, int siteNo, DateTime startDate, DateTime endDate) 
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = "INSERT INTO [dbo].[CustomerBooking] (" +
                            "BillingAddress, SiteNo, StartDate, EndDate, CustomerEmail) " +
                         "VALUES (" +
                            "@BillingAddress, " +
                            "@SiteNo, " +
                            "@StartDate, " +
                            "@EndDate, " +
                            "@CustomerEmail);";

            SqlCommand command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("@CustomerEmail", email);
            command.Parameters.AddWithValue("@BillingAddress", await getCustomerAddressFromEmail(email));
            command.Parameters.AddWithValue("@SiteNo", siteNo);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate", endDate);

            con.Open();
            return await Task.FromResult(command.ExecuteNonQuery());
        }

        public async Task<int> InsertCampsiteMaintenance(NewEmployeeMaintenanceModel request)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String insertNewCampsiteMaintenanceSQL = "INSERT INTO [dbo].[CampsiteMaintenance] (SiteNo, ESSN, Date, Description) " +
                                                     "VALUES (@SiteNo, @ESSN, @Date, @Description);";
            SqlCommand command = new SqlCommand(insertNewCampsiteMaintenanceSQL, con);

            command.Parameters.AddWithValue("@SiteNo", request.SiteNo);
            command.Parameters.AddWithValue("@ESSN", request.ESSN);
            command.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Description", request.Description);

            con.Open();
            return await Task.FromResult(command.ExecuteNonQuery());
        }
    }
}
