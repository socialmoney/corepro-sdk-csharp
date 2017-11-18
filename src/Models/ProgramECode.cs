using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePro.SDK.Models
{
    public class ProgramECode : ProductBase
    {
        public new string Category { get; set; }
        public new string Type { get; set; }
        public int? ProgramECodeId { get; set; }
        public string ProductCode { get; set; }
        public decimal? MinimumAmount { get; set; }
        public decimal? MaximumAmount { get; set; }
        public decimal? PercentToCustomer { get; set; }
        public Dictionary<string, string> Name { get; set; }
        public Dictionary<string, string> ImageUrl { get; set; }
        public bool? IsReissueSupported { get; set; }
        public bool? IsCancelSupported { get; set; }

        public ProgramECode()
            : base()
        {
            Name = new Dictionary<string, string>();
            ImageUrl = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            return "eCodeId:" + ProgramECodeId.ToString() + ", ProductCode:" + ProductCode + ", MinimumAmount:" + MinimumAmount.ToString() + ", MaximumAmount:" + MaximumAmount.ToString() + ", PercentToCustomer:" + PercentToCustomer.ToString() + ", Names:" + Name + ", ImageUrls:" + ImageUrl + ", IsReissueSupported:" + IsReissueSupported.ToString() + ", IsCancelSupported:" + IsCancelSupported.ToString();
        }
    }
}
