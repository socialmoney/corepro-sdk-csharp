using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class ProgramInterestRate
    {
        public int? Tier { get; set; }
        public decimal? Apy { get; set; }
        public decimal? Apr { get; set; }
        public decimal? MinimumAmount { get; set; }
        public decimal? MaximumAmount { get; set; }

        public DateTimeOffset? RateEffectiveDate { get; set; }
        public DateTimeOffset? RateExpireDate { get; set; }
        public DateTimeOffset? EffectiveDate { get; set; }
        public DateTimeOffset? ExpireDate { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Tier:{Tier}, Description:{Description}, Min:{MinimumAmount}, Max:{MaximumAmount}, Apr:{Apr}, Apy:{Apy}, RateEffective:{RateEffectiveDate}, RateExpireDate:{RateExpireDate}";
        }
    }
}
