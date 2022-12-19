using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using CampgroundBooking.Backend.Models.EmployeeModel;
using CampgroundBooking.Backend.Models;

namespace CampgroundBooking.Backend.NewEmployeeUtlis
{
    public class NewEmployeeDBServices
    {
        private readonly IConfiguration config;

        public NewEmployeeDBServices(IConfiguration cfg)
        {
            config = cfg;
        }
        private string dbConnectionString { get { return config.GetConnectionString("Default"); } }

        public async Task<int> InsertEmployeeTask(NewEmployeeTaskModel model)
        {
            List<PersonEmailModel> emails = new();
            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);

            String sql = $"select Employee.ESSN from dbo.Employee where Employee.Email = '{model.Email}'";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);

            foreach (DataRow row in dbQuereyResult.Rows)
            {
                int essn = (int)row["ESSN"];
                model.ESSN = essn;
            }

            sql = @"insert into dbo.EmployeeTasks (ESSN, Description)
                            values (@ESSN, @Description);";

            String sql2 = @"insert into dbo.EmployeeTaskAssigned (ESSN, TaskId)
                            values (@ESSN, @TaskId);";

            SqlCommand command1 = new SqlCommand(sql, con);
            SqlCommand command2 = new SqlCommand(sql2, con);

            command1.Parameters.AddWithValue("@ESSN", model.ESSN);
            command1.Parameters.AddWithValue("@Description", model.Description);

            con.Open();
            if (command1.ExecuteNonQuery() != 1) { return await Task.FromResult(-1); }

            command2.Parameters.AddWithValue("@ESSN", model.ESSN);
            command2.Parameters.AddWithValue("@TaskId", await getNewestTaskId());


            return await Task.FromResult(command2.ExecuteNonQuery());
        }

        public async Task<int> getNewestTaskId()
        {
            int taksId = -1;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SELECT MAX(TaskId) FROM [dbo].[EmployeeTasks]", con);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows) { taksId = ((row["Column1"] == DBNull.Value)) ? 1 : (int)row["Column1"]; }
            return await Task.FromResult(taksId);
        }

        public async Task<int> InsertEmployeeWorkLoaction(NewEmployeeWorkLocationModel model)
        {
            List<PersonEmailModel> emails = new();
            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);

            String sql = $"select Employee.ESSN from dbo.Employee where Employee.Email = '{model.Email}'";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);

            foreach (DataRow row in dbQuereyResult.Rows)
            {
                int essn = (int)row["ESSN"];
                model.ESSN = essn;
            }

            sql = @"insert into dbo.EmployeeWorkLocation (ESSN, CampgroundId)
                            values (@ESSN, @CampgroundId);";

            SqlCommand command1 = new SqlCommand(sql, con);
            command1.Parameters.AddWithValue("@ESSN", model.ESSN);
            command1.Parameters.AddWithValue("@CampgroundId", model.CampgroundId);
            con.Open();
            return await Task.FromResult(command1.ExecuteNonQuery());
        }

        public async Task<int> InsertEmployee(NewEmployeeModel model)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String sql = @"insert into dbo.Employee (ESSN, Age, Sex, Email)
                            values (@ESSN, @Age, @Sex, @Email);";

            SqlCommand command1 = new SqlCommand(sql, con);

            command1.Parameters.AddWithValue("@ESSN", model.ESSN);
            command1.Parameters.AddWithValue("@Age", model.Age);
            command1.Parameters.AddWithValue("@Sex", model.Sex);
            command1.Parameters.AddWithValue("@Email", model.Email);

            con.Open();
            return await Task.FromResult(command1.ExecuteNonQuery());
        }

        public async Task<List<PersonEmailModel>> GetAllEmails()
        {
            List<PersonEmailModel> emails = new();
            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);


            string? adminEmail = CurrentUserModel.GetAdminUser();
            string sql = $"select Person.Email from dbo.Person where Person.Email != '{adminEmail}' and Person.Email not in " +
                "(select Employee.Email from dbo.Employee)";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);

            foreach(DataRow row in dbQuereyResult.Rows)
            {
                string email = (string)row["Email"];

                PersonEmailModel currPerson = new PersonEmailModel(email);
                emails.Add(currPerson);
            }
            return await Task.FromResult(emails);
        }

        public async Task<List<EmployeeSSNsModel>> GetEmployeeSSNs()
        {
            List<EmployeeSSNsModel> ssns = new();
            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);

            string sql = $"select Employee.ESSN from dbo.Employee";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);

            foreach (DataRow row in dbQuereyResult.Rows)
            {
                int ssn = (int)row["ESSN"];

                EmployeeSSNsModel currESSN = new EmployeeSSNsModel(ssn);
                ssns.Add(currESSN);
            }

            return await Task.FromResult(ssns);
        }

        public async Task<List<PersonEmailModel>> GetEmployeeEmails()
        {
            List<PersonEmailModel> emails = new();
            DataTable dbQuereyResult = new DataTable();
            SqlConnection con = new SqlConnection(dbConnectionString);

            string sql = $"select Employee.Email from dbo.Employee";

            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            da.Fill(dbQuereyResult);

            foreach (DataRow row in dbQuereyResult.Rows)
            {
                string email = (string)row["Email"];

                PersonEmailModel currPerson = new PersonEmailModel(email);
                emails.Add(currPerson);
            }

            return await Task.FromResult(emails);
        }

        public async Task<int> DeleteEemployee(string emp)
        {
            SqlConnection con = new SqlConnection(dbConnectionString);
            String DeleteCampsiteSQL = "DELETE FROM [dbo].[Employee] " +
                                       "WHERE Email=@Email";

            SqlCommand command = new SqlCommand(DeleteCampsiteSQL, con);

            con.Open();

            command.Parameters.AddWithValue("@Email", emp);

            return await Task.FromResult(command.ExecuteNonQuery());
        }
    }
}
