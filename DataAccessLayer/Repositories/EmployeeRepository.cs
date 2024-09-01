using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
	    private readonly IDbWrapper<Employee> _employeeDbWrapper;

	    public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
	    {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            var employeeRecords =  await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
            return employeeRecords?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var employeeRecord = (await _employeeDbWrapper.FindAsync(t =>
                t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) && t.EmployeeCode.Equals(employee.EmployeeCode)))?.FirstOrDefault();

            if (employeeRecord != null)
            {
                employeeRecord.EmployeeCode = employee.EmployeeCode;
                employeeRecord.EmployeeStatus = employee.EmployeeStatus;
                employeeRecord.EmailAddress = employee.EmailAddress;
                employeeRecord.Occupation = employee.Occupation;
                employeeRecord.EmployeeStatus = employee.EmployeeStatus;
                employeeRecord.Phone = employee.Phone;
                employeeRecord.LastModified = employee.LastModified;
                return _employeeDbWrapper.Update(employeeRecord);
            }

            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            return await _employeeDbWrapper.DeleteAsync(c => c.EmployeeCode.Equals(employeeCode));
        }
    }
}
