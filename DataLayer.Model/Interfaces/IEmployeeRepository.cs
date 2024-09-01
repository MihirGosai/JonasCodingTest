using DataAccessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByCodeAsync(string employeeCode);
        Task<bool> SaveEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(string employeeCode);
    }
}
