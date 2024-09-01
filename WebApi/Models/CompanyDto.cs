using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class CompanyDto : BaseDto
    {
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EquipmentCompanyCode { get; set; }
        public string Country { get; set; }
        public List<ArSubledgerDto> ArSubledgers { get; set; }
        public DateTime LastModified { get; set; }
    }
}