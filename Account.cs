using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class Account : ModelBase
    {

        public Account()
        {

        }

        public Account(int? customerId, int? accountId)
            : this()
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNumberMasked { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? ClosedDate { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? AccountBalance { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsCloseable { get; set; }

        public string RoutingNumber { get; set; }

        public decimal? TargetAmount { get; set; }
        public DateTimeOffset? TargetDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Tag { get; set; }
        public string Miscellaneous { get; set; }

        public string RecurringContributionType { get; set; }
        public decimal? RecurringContributionAmount { get; set; }
        public int? RecurringContributionFromExternalAccountId { get; set; }
        public DateTimeOffset? RecurringContributionStartDate { get; set; }
        public DateTimeOffset? RecurringContributionEndDate { get; set; }
        public DateTimeOffset? RecurringContributionNextDate { get; set; }

        public static List<Account> List(int? customerId, int? accountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Account(customerId, accountId).List(connection, userDefinedObjectForLogging);
        }

        public virtual List<Account> List(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<Account>>(String.Format("account/list/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static Account Get(int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Account(customerId, accountId).Get(connection, userDefinedObjectForLogging);
        }

        public virtual Account Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Account>(String.Format("account/get/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static Account GetByTag(int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var a = new Account(customerId, null);
            a.Tag = tag;
            return a.GetByTag(connection, userDefinedObjectForLogging);
        }

        public virtual Account GetByTag(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Account>(String.Format("account/getbytag/{0}/{1}", this.CustomerId, this.Tag), connection, userDefinedObjectForLogging);
            return rv;
        }


        public static bool Update(int? customerId, int? accountId, string name = null, string tag = null, 
            string category = null, string subCategory = null, string miscellaneous = null,
            decimal? targetAmount = null, DateTimeOffset? targetDate = null, 
            string recurringContributionType = null, decimal? recurringContributionAmount = null, int? recurringContributionFromExternalAccountId = null,
            DateTimeOffset? recurringContributionStartDate = null, DateTimeOffset? recurringContributionEndDate = null, bool? isCloseable = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var a = new Account(customerId, accountId);
            a.Name = name;
            a.TargetAmount = targetAmount;
            a.TargetDate = targetDate;
            a.Category = category;
            a.SubCategory = subCategory;
            a.Tag = tag;
            a.Miscellaneous = miscellaneous;
            a.RecurringContributionType = recurringContributionType;
            a.RecurringContributionAmount = recurringContributionAmount;
            a.RecurringContributionStartDate = recurringContributionStartDate;
            a.RecurringContributionFromExternalAccountId = recurringContributionFromExternalAccountId;
            a.RecurringContributionEndDate = recurringContributionEndDate;
            a.IsCloseable = isCloseable;
            return a.Update(connection, userDefinedObjectForLogging);
        }

        public virtual bool Update(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            Requestor.Post<object>("account/update", connection, this, userDefinedObjectForLogging);
            return true;
        }

        public static int Create(int? customerId, int? accountId, string name, string type, string tag = null,
            string category = null, string subCategory = null, string miscellaneous = null,
            decimal? targetAmount = null, DateTimeOffset? targetDate = null,
            string recurringContributionType = null, decimal? recurringContributionAmount = null, int? recurringContributionFromExternalAccountId = null,
            DateTimeOffset? recurringContributionStartDate = null, DateTimeOffset? recurringContributionEndDate = null, bool? isCloseable = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var a = new Account(customerId, accountId);
            a.Name = name;
            a.TargetAmount = targetAmount;
            a.TargetDate = targetDate;
            a.Category = category;
            a.SubCategory = subCategory;
            a.Tag = tag;
            a.Type = type;
            a.Miscellaneous = miscellaneous;
            a.RecurringContributionType = recurringContributionType;
            a.RecurringContributionAmount = recurringContributionAmount;
            a.RecurringContributionStartDate = recurringContributionStartDate;
            a.RecurringContributionFromExternalAccountId = recurringContributionFromExternalAccountId;
            a.RecurringContributionEndDate = recurringContributionEndDate;
            a.IsCloseable = isCloseable;
            return a.Create(connection, userDefinedObjectForLogging);
        }

        public virtual int Create(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<dynamic>("account/create", connection, this, userDefinedObjectForLogging);
            this.AccountId = (int)rv.accountId;
            return (int)this.AccountId;
        }

        public static AccountClose Close(int? customerId, int? accountId, int? closeToAccountId, string transactionTag, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Account(customerId, accountId).Close(closeToAccountId, transactionTag, connection, userDefinedObjectForLogging);
        }

        public virtual AccountClose Close(int? closeToAccountId, string transactionTag, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = AccountClose.Close(this.CustomerId, this.AccountId, closeToAccountId, transactionTag, connection, userDefinedObjectForLogging);
            return rv;
        }


    }
}
