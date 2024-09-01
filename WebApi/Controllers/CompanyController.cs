using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using NLog;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET api/<controller>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            Logger.Info("Fetching all companies");

            try
            {
                var items = await _companyService.GetAllCompaniesAsync();
                Logger.Info("Fetched all companies now.");
                return Ok(_mapper.Map<IEnumerable<CompanyDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to get all companies.");
                return InternalServerError(ex);
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{companyCode}")]
        public async Task<IHttpActionResult> Get(string companyCode)
        {
            Logger.Info($"Fetching copmany by companycode: {companyCode}.");

            try
            {
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);

                if (item != null)
                {
                    Logger.Info($"Company with companycode: {companyCode} is fetched now.");
                    return Ok(_mapper.Map<CompanyDto>(item));
                }
                else
                {
                    Logger.Info($"No company found by companycode: {companyCode}.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to get copmany by companycode: {companyCode}.");
                return InternalServerError(ex);
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                Logger.Info($"Payload was invalid to create Company");
                return BadRequest("Invalid request.");
            }

            Logger.Info($"Going to create new copmany with companycode: {companyDto.CompanyCode}.");

            try
            {
                var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
                var result = await _companyService.SaveCompanyAsync(companyInfo);
                if (result)
                {
                    Logger.Info($"Company with companycode: {companyDto.CompanyCode} is created now.");
                    return Ok(companyDto);
                }
                else
                {
                    Logger.Error($"Failed to create copmany with companycode: {companyDto.CompanyCode}.");
                    return BadRequest("Unable to create company.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to create copmany with companycode: {companyDto.CompanyCode}.");
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("{companyCode}")]
        public async Task<IHttpActionResult> Put(string companyCode, [FromBody] CompanyDto companyDto)
        {
            if (string.IsNullOrEmpty(companyCode) || companyDto == null)
            {
                Logger.Info($"Payload was invalid to update Company");
                return BadRequest("Invalid request.");
            }

            Logger.Info($"Going to update copmany with companycode: {companyCode}.");

            try
            {
                companyDto.CompanyCode = companyCode;
                var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
                var result = await _companyService.SaveCompanyAsync(companyInfo);
                if (result)
                {
                    Logger.Info($"Company with companycode: {companyCode} is updated now.");
                    return Ok(companyDto);
                }
                else
                {
                    Logger.Error($"Failed to update copmany with companycode: {companyDto.CompanyCode}.");
                    return BadRequest("Unable to update company.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to update copmany with companycode: {companyDto.CompanyCode}.");
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{companyCode}")]
        public async Task<IHttpActionResult> Delete(string companyCode)
        {
            Logger.Info($"Going to delete copmany with companycode: {companyCode}.");

            try
            {
                var company = await _companyService.GetCompanyByCodeAsync(companyCode);
                if (company == null)
                {
                    Logger.Info($"No company found with companycode: {companyCode}.");
                    return NotFound();
                }

                var result = await _companyService.DeleteCompanyAsync(companyCode);
                if (result)
                {
                    Logger.Info($"Company with companycode: {companyCode} is deleted now.");
                    return Ok();
                }

                Logger.Error($"Failed to delete with companycode: {companyCode}.");
                return BadRequest("Unable to delete company.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to delete company with companycode: {companyCode}.");
                return InternalServerError(ex);
            }
        }
    }
}