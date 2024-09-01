using System;

namespace DataAccessLayer.Model.Models
{
    public class ArSubledger : DataEntity
    {
        public string ArSubledgerCode { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostalZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public DateTime? LastModified { get; set; }
        public string Active { get; set; }
        public string Inactive { get; set; }
        public string Excellent { get; set; }
        public string Good { get; set; }
        public string Fair { get; set; }
        public string Poor { get; set; }
        public string Condemned { get; set; }
    }
}