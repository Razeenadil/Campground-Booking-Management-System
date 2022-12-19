using CampgroundBooking.Backend.Models.EmployeeModel;

namespace CampgroundBooking.Backend.Interfaces
{
    public interface IEmployeeData
    {
        Task<List<EmployeeEmailsModel>> GetEmployeeEmails();

        Task<List<EmployeeTaskModel>> GetEmployeeTasks(string userEmail);

        Task<List<string>> GetEmployeeWorkLocations(string userEmail);

        Task<List<EmployeeMaintenanceModel>> GetMaintenance(string userEmail);
    }
}