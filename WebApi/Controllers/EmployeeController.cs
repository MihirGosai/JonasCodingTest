using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public EmployeeController(IEmployeeService employeeService, ICompanyService companyService, IMapper mapper)
        {
            _employeeService = employeeService;
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            Logger.Info("Fetching all employees");

            try
            {
                var employeeRecords = await _employeeService.GetAllEmployeesAsync();

                var employeeRecordsToReturn = new List<EmployeeReadDto>();

                foreach (var employee in employeeRecords)
                {
                    var employeeDto = new EmployeeReadDto
                    {
                        EmployeeCode = employee.EmployeeCode,
                        EmployeeName = employee.EmployeeName,
                        CompanyName = await _companyService.GetCompanyNameByCodeAsync(employee.CompanyCode),
                        OccupationName = employee.Occupation,
                        EmployeeStatus = employee.EmployeeStatus,
                        EmailAddress = employee.EmailAddress,
                        PhoneNumber = employee.Phone,
                        LastModifiedDateTime = employee.LastModified.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    employeeRecordsToReturn.Add(employeeDto);
                }

                Logger.Info("Fetched all employees now.");
                return Ok(employeeRecordsToReturn);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to get all employees.");
                return InternalServerError(ex);
            }
            
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> Get(string employeeCode)
        {
            try
            {
                Logger.Info($"Fetching employee by employee code {employeeCode}.");

                var employee = await _employeeService.GetEmployeeByCodeAsync(employeeCode);

                if (employee != null)
                {
                    var employeeDto = new EmployeeReadDto
                    {
                        EmployeeCode = employee.EmployeeCode,
                        EmployeeName = employee.EmployeeName,
                        CompanyName = await _companyService.GetCompanyNameByCodeAsync(employee.CompanyCode),
                        OccupationName = employee.Occupation,
                        EmployeeStatus = employee.EmployeeStatus,
                        EmailAddress = employee.EmailAddress,
                        PhoneNumber = employee.Phone,
                        LastModifiedDateTime = employee.LastModified.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    Logger.Info($"Employee with emplyee code: {employeeCode} is fetched now.");
                    return Ok(employeeDto);
                }
                else
                {
                    Logger.Info($"No employee found by emplyee code: {employeeCode}.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get employee with employee code {employeeCode}");
                return InternalServerError(ex);
            }

        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                Logger.Info($"Payload was invalid to create employee");
                return BadRequest("Invalid request.");
            }

            Logger.Info($"Going to create new employee with employeecode: {employeeDto.EmployeeCode}.");

            try
            {
                var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                var result = await _employeeService.SaveEmployeeAsync(employeeInfo);
                if (result)
                {
                    Logger.Info($"Employee with employee code: {employeeDto.EmployeeCode} is created now.");
                    return Ok(employeeDto);
                }
                else
                {
                    Logger.Error($"Failed to create employee with employee code: {employeeDto.EmployeeCode}.");
                    return BadRequest("Unable to create employee.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to create employee with employee code: {employeeDto.EmployeeCode}.");
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> Put(string employeeCode, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                Logger.Info($"Payload was invalid to update Employee");
                return BadRequest("Invalid request.");
            }

            Logger.Info($"Going to update employee with employee code: {employeeCode}.");

            try
            {
                employeeDto.EmployeeCode = employeeCode;
                var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                var result = await _employeeService.SaveEmployeeAsync(employeeInfo);
                if (result)
                {
                    Logger.Info($"Employee with employee code: {employeeCode} is updated now.");
                    return Ok(employeeDto);
                }
                else
                {
                    Logger.Error($"Failed to update employee with employee code: {employeeDto.EmployeeCode}.");
                    return BadRequest("Unable to update employee.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to update employee with employee code: {employeeDto.EmployeeCode}.");
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{employeeCode}")]
        public async Task<IHttpActionResult> Delete(string employeeCode)
        {
            Logger.Info($"Going to delete copmany with companycode: {employeeCode}.");

            try
            {
                var employee = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
                if (employee == null)
                {
                    Logger.Info($"No employee found with employee code: {employeeCode}.");
                    return NotFound();
                }

                var result = await _employeeService.DeleteEmployeeAsync(employeeCode);
                if (result)
                {
                    Logger.Info($"Employee with employee code: {employeeCode} is deleted now.");
                    return Ok();
                }

                Logger.Error($"Failed to delete with employee with employee code: {employeeCode}.");
                return BadRequest("Unable to delete employee.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}