using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class Program : ModelBase
    {
        public Program()
            : base()
        {
            ECodeProducts = new SortedDictionary<string, ProgramECode>();
            CheckingProducts = new SortedDictionary<string, ProgramChecking>();
            SavingsProducts = new SortedDictionary<string, ProgramSavings>();
            PrepaidProducts = new SortedDictionary<string, ProgramPrepaid>();
        }
        public string Name { get; set; }
        public string VerificationType { get; set; }
        public string TimeZone { get; set; }

        public ProgramLimit PerUserDailyWithdrawLimit { get; set; }
        public ProgramLimit PerUserMonthlyWithdrawLimit { get; set; }
        public ProgramLimit PerProgramDailyWithdrawLimit { get; set; }

        public ProgramLimit PerUserDailyDepositLimit { get; set; }
        public ProgramLimit PerUserMonthlyDepositLimit { get; set; }
        public ProgramLimit PerProgramDailyDepositLimit { get; set; }

        public string Website { get; set; }
        public bool IsInternalToInternalTransferEnabled { get; set; }
        public int DecimalCount { get; set; }
        public List<string> ValidAccountTypes { get; set; }

        public int PerUserExternalAccountCountMax { get; set; }
        public int PerUserAccountCountMax { get; set; }
        public decimal PerUserTotalAccountBalanceMax { get; set; }

        public SortedDictionary<string, ProgramECode> ECodeProducts { get; set; }
        public SortedDictionary<string, ProgramChecking> CheckingProducts { get; set; }
        public SortedDictionary<string, ProgramSavings> SavingsProducts { get; set; }
        public SortedDictionary<string, ProgramPrepaid> PrepaidProducts { get; set; }

        public DateTimeOffset FilledDate { get; set; }

        public Bank Bank { get; set; }

        public static Program Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Program>("program/get", connection, userDefinedObjectForLogging);
            return rv;
        }


        public async static Task<Program> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Program>(cancellationToken, "program/get", connection, userDefinedObjectForLogging);
            return rv.Data;
        }

    }
}
