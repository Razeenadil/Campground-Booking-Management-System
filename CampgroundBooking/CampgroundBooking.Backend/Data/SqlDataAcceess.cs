using CampgroundBooking.Backend.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace CampgroundBooking.Backend.Data
{
    public class SqlDataAcceess : ISqlDataAcceess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAcceess(IConfiguration config)
        {
            _config = config;
        }

        //loads data from the database
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        //runs on database inserts, updates, delete
        public async Task SaveData<T>(string sql, T parameter)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameter);
            }
        }
    }
}
