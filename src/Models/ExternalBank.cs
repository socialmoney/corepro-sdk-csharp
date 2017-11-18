using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class ExternalBank : ModelBase
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string BankName { get; set; }
        public string AchRoutingNumber { get; set; }
        public string LocationType { get; set; }
        public string PhysicalAddressLine1 { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalState { get; set; }
        public string PhysicalPostalCode { get; set; }
        public string MainPhoneNumber { get; set; }
        public string MainFaxNumber { get; set; }
        public string MainUrl { get; set; }
        public string MainEmailAddress { get; set; }
        public string CustomerServicePhoneNumber { get; set; }
        public string SwiftIdentifier { get; set; }
        public string TelexIdentifier { get; set; }
        public string CorrespondentBankRoutingNumber { get; set; }
        public string CorrespondentBankName { get; set; }
    }
}
