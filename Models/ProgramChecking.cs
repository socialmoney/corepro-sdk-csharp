using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePro.SDK.Models
{
    public class ProgramChecking : ProductBase
    {
        //public new string Category { get; set; }
        //public new string Type { get; set; }
        public ProgramLimit BalanceLimit { get; set; }
        public List<ProgramInterestRate> InterestRates { get; set; }
        public bool IsExternalWithdrawEnabled { get; set; }
        public bool IsInterestEnabled { get; set; }
        public bool IsRecurringContributionEnabled { get; set; }
        public ProgramLimit PerTransactionDepositLimit { get; set; }
        public ProgramLimit PerTransactionWithdrawLimit { get; set; }

        public ProgramChecking()
            : base()
        {
            InterestRates = new List<ProgramInterestRate>();
        }
    }
}
