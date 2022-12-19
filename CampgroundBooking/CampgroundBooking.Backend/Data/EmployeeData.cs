using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Backend.Models.EmployeeModel;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;



namespace CampgroundBooking.Backend.Data
{
    public class EmployeeData : IEmployeeData
    {
        private readonly ISqlDataAcceess _db;
        private readonly IConfiguration config;

        public EmployeeData(ISqlDataAcceess db, IConfiguration cfg)
        {
            _db = db;
            config = cfg;
        }

        private string dbConnectionString { get { return config.GetConnectionString("Default"); } }

        public Task<List<EmployeeEmailsModel>> GetEmployeeEmails()
        {
            string sql = "select Email from dbo.Employee";
            return _db.LoadData<EmployeeEmailsModel, dynamic>(sql, new { });
        }

        public async Task<List<EmployeeTaskModel>> GetEmployeeTasks(string userEmail)
        {
            string sql = $"select ESSN from dbo.Employee where Employee.Email = '{userEmail}'";

            List<int> essn = await _db.LoadData<int, dynamic>(sql, new { });

            foreach (var e in essn)
            {
                sql = $"select TaskId, Description from dbo.EmployeeTasks where EmployeeTasks.ESSN = {e}";

            }
            return await _db.LoadData<EmployeeTaskModel, dynamic>(sql, new { });
        }

        public async Task<List<string>> GetEmployeeWorkLocations(string userEmail)
        {
            List<string> locs = new();
            string sql = $"select ESSN from dbo.Employee where Employee.Email = '{userEmail}'";

            List<int> essn = await _db.LoadData<int, dynamic>(sql, new { });

            foreach (var e in essn)
            {
                sql = $"select CampgroundId from dbo.EmployeeWorkLocation where EmployeeWorkLocation.ESSN = {e}";
            }

            List<int> cgId = await _db.LoadData<int, dynamic>(sql, new { });

            foreach (var id in cgId)
            {
                sql = $"select Name from dbo.Campground where Campground.Id = {id}";
                var x = await _db.LoadData<EmployeeWorkLocationModel, dynamic>(sql, new { });

                foreach (var w in x)
                {
                    locs.Add(w.Name);
                }
            }

            return locs;
        }

        public async Task<List<EmployeeMaintenanceModel>> GetMaintenance(string userEmail)
        {
            string sql = $"select ESSN from dbo.Employee where Employee.Email = '{userEmail}'";

            List<int> essn = await _db.LoadData<int, dynamic>(sql, new { });

            foreach (var e in essn)
            {
                sql = $"select SiteNo, MaintenanceNumber, Date, Description from dbo.CampsiteMaintenance where CampsiteMaintenance.ESSN = {e}";
            }


            return await _db.LoadData<EmployeeMaintenanceModel, dynamic>(sql, new { });
        }

    }
}
