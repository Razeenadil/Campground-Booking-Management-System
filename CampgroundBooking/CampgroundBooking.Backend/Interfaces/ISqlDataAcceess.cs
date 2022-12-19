
namespace CampgroundBooking.Backend.Interfaces
{
    public interface ISqlDataAcceess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameter);
    }
}