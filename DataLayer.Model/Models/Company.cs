using System;
using System.Collections.Generic;

namespace DataAccessLayer.Model.Models
{
    public class Company : DataEntity
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
        public List<ArSubledger> ArSubledgers { get; set; }
        public DateTime LastModified { get; set; }
    }
}
