using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class Program : ModelBase
    {
        public Program()
            : base()
        {
            InterestRates = new List<ProgramInterestRate>();
        }
        public string Name { get; set; }
        public string VerificationType { get; set; }
        public string TimeZone { get; set; }

        public decimal RegDFeeAmount { get; set; }
        public decimal RegDMonthlyTransactionWithdrawCountMax { get; set; }

        public ProgramLimit PerTransactionWithdrawLimit { get; set; }
        public ProgramLimit PerUserDailyWithdrawLimit { get; set; }
        public ProgramLimit PerUserMonthlyWithdrawLimit { get; set; }
        public ProgramLimit PerProgramDailyWithdrawLimit { get; set; }

        public ProgramLimit PerTransactionDepositLimit { get; set; }
        public ProgramLimit PerUserDailyDepositLimit { get; set; }
        public ProgramLimit PerUserMonthlyDepositLimit { get; set; }
        public ProgramLimit PerProgramDailyDepositLimit { get; set; }

        public string Website { get; set; }
        public bool IsInternalToInternalTransferEnabled { get; set; }
        public int DecimalCount { get; set; }
        public bool IsInterestEnabled { get; set; }
        public string AllowedAccountType { get; set; }
        public bool IsRecurringContributionEnabled { get; set; }

        public List<ProgramInterestRate> InterestRates { get; set; }

        public DateTimeOffset FilledDate { get; set; }

        public static Program Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Program>("program/get", connection, userDefinedObjectForLogging);
            return rv;
        }

    }
}
