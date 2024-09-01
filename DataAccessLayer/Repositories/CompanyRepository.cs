using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            var companyRecords =  await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
            return companyRecords?.FirstOrDefault();
        }

        public async Task<string> GetCompanyNameByCodeAsync(string companyCode)
        {
            var companyRecord = (await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode)))?.FirstOrDefault();
            return companyRecord?.CompanyName;
        }

        public async Task<bool> SaveCompanyAsync(Company company)
        {
            var companyRecord = (await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode)))?.FirstOrDefault();

            if (companyRecord != null)
            {
                companyRecord.CompanyName = company.CompanyName;
                companyRecord.AddressLine1 = company.AddressLine1;
                companyRecord.AddressLine2 = company.AddressLine2;
                companyRecord.AddressLine3 = company.AddressLine3;
                companyRecord.Country = company.Country;
                companyRecord.EquipmentCompanyCode = company.EquipmentCompanyCode;
                companyRecord.FaxNumber = company.FaxNumber;
                companyRecord.PhoneNumber = company.PhoneNumber;
                companyRecord.PostalZipCode = company.PostalZipCode;
                companyRecord.LastModified = company.LastModified;
                return _companyDbWrapper.Update(companyRecord);
            }

            return await _companyDbWrapper.InsertAsync(company);
        }

        public async Task<bool> DeleteCompanyAsync(string companyCode)
        {
            var result = await _companyDbWrapper.DeleteAsync(c => c.CompanyCode.Equals(companyCode));
            return result;
        }
    }
}
