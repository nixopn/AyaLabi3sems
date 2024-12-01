using WebAPIModels.Models;

namespace SQLServerWebApiConsoleClient.Repositories
{
    public interface ICustomerRepository
    {
        Task<Employee?> GetAsync(int customerId);
        Task<Employee> UpdateAsync(int customerId, Employee customer);
        Task<bool?> DeleteAsync(int customerId);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> CreateAsync(Employee customer);
    }
}
