using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class Account : ModelBase
    {

        public override string ToString()
        {
            return "Id: " + this.AccountId + ", NumberMasked: " + this.AccountNumberMasked + ", CardPriority: " + this.CardPriority;
        }

        public Account() : base()
        {

        }
        public Account(RequestMetaData metaData) : base(metaData)
        {

        }

        public Account(int? customerId, int? accountId, RequestMetaData metaData = null)
            : this(metaData)
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
        public decimal? ClosedAmount { get; set; }
        public long? ClosedTransactionId { get; set; }
        public string ClosedReason { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? AccountBalance { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsCloseable { get; set; }

        public string RoutingNumber { get; set; }
        public string RoutingNumberMasked { get; set; }

        public decimal? TargetAmount { get; set; }
        public DateTimeOffset? TargetDate { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Tag { get; set; }

        public string RecurringContributionType { get; set; }
        public decimal? RecurringContributionAmount { get; set; }
        public int? RecurringContributionFromExternalAccountId { get; set; }
        public DateTimeOffset? RecurringContributionStartDate { get; set; }
        public DateTimeOffset? RecurringContributionEndDate { get; set; }
        public DateTimeOffset? RecurringContributionNextDate { get; set; }

        public int? RegDWithdrawalCount { get; set; }

        public DateTimeOffset? TargetMetDate { get; set; }
        public decimal? TargetMetPercent { get; set; }

        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }

        public decimal? PendingBalance { get; set; }

        public int? PrimaryCustomerId { get; set; }
        public string LegalName1 { get; set; }
        public string LegalName2 { get; set; }
        public string AccessTypeCode { get; set; }
        public int? TotalCustomers { get; set; }
        public bool? IsJointAccount { get; set; }

        public bool? IsPrimaryCustomer { get; set; }

        public decimal? InterestApr { get; set; }
        public decimal? InterestApy { get; set; }
        public int? Tier { get; set; }
        public string TierDescription { get; set; }
        public decimal? TierMinimumAmount { get; set; }
        public decimal? TierMaximumAmount { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }
        public DateTimeOffset? BalanceLastModifiedDate { get; set; }

        public string ExternalProgramTag { get; set; }

        /// <summary>
        /// Populated only when Account is tied to a Card
        /// </summary>
        public int? CardPriority { get; set; }

        /// <summary>
        /// Can be set via API only by calling /account/addAccess or /account/editAccess.  Defaults to 1 when creating an account.
        /// </summary>
        public int? CustomerPriority { get; set; }

        #region Async
        public async static Task<List<Account>> ListAsync(CancellationToken cancellationToken, int? customerId, int? accountId = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Account(customerId, accountId, metaData).ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging)).Data;
        }

        public async static Task<Envelope<List<Account>>> ListAsyncEnvelope(CancellationToken cancellationToken, int? customerId, int? accountId = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Account(customerId, accountId, metaData).ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging));
        }

        public async virtual Task<List<Account>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async virtual Task<Envelope<List<Account>>> ListAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Account>>(cancellationToken, String.Format("account/list/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public async static Task<Account> GetAsync(CancellationToken cancellationToken, int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Account(customerId, accountId, metaData).GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async static Task<Envelope<Account>> GetAsyncEnvelope(CancellationToken cancellationToken, int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Account(customerId, accountId, metaData).GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData));
        }

        public async virtual Task<Account> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await this.GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async virtual Task<Envelope<Account>> GetAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Account>(cancellationToken, String.Format("account/get/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public async static Task<Account> GetByTagAsync(CancellationToken cancellationToken, int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, null, metaData);
            a.Tag = tag;
            return (await a.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging)).Data;
        }

        public async static Task<Envelope<Account>> GetByTagAsyncEnvelope(CancellationToken cancellationToken, int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, null, metaData);
            a.Tag = tag;
            return (await a.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging));
        }

        public async virtual Task<Account> GetByTagAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await this.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging)).Data;
        }

        public async virtual Task<Envelope<Account>> GetByTagAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Account>(cancellationToken, String.Format("account/getbytag/{0}/?tag={1}", this.CustomerId, Uri.EscapeDataString(this.Tag + "")), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public async static Task<Account> UpdateAsync(CancellationToken cancellationToken, int? customerId, int? accountId, string name = null, string tag = null,
            string category = null, string subCategory = null, string miscellaneous = null /* no longer used, remains only to prevent breaking interface */ ,
            decimal? targetAmount = null, DateTimeOffset? targetDate = null,
            string recurringContributionType = null, decimal? recurringContributionAmount = null, int? recurringContributionFromExternalAccountId = null,
            DateTimeOffset? recurringContributionStartDate = null, DateTimeOffset? recurringContributionEndDate = null, bool? isCloseable = null,
            Connection connection = null, object userDefinedObjectForLogging = null, string externalProgramTag = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, accountId, metaData);
            a.Name = name;
            a.TargetAmount = targetAmount;
            a.TargetDate = targetDate;
            a.Category = category;
            a.SubCategory = subCategory;
            a.Tag = tag;
            a.RecurringContributionType = recurringContributionType;
            a.RecurringContributionAmount = recurringContributionAmount;
            a.RecurringContributionStartDate = recurringContributionStartDate;
            a.RecurringContributionFromExternalAccountId = recurringContributionFromExternalAccountId;
            a.RecurringContributionEndDate = recurringContributionEndDate;
            a.IsCloseable = isCloseable;
            a.ExternalProgramTag = externalProgramTag;
            return await a.UpdateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Account> UpdateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Account>(cancellationToken, "account/update", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.Data.RequestId;
            return rv.Data;
        }

        public async static Task<Account> CreateAsync(CancellationToken cancellationToken, int? customerId, int? accountId, string name, string type, string tag = null,
            string category = null, string subCategory = null, string miscellaneous = null /* no longer used, remains only to prevent breaking interface */ ,
            decimal? targetAmount = null, DateTimeOffset? targetDate = null,
            string recurringContributionType = null, decimal? recurringContributionAmount = null, int? recurringContributionFromExternalAccountId = null,
            DateTimeOffset? recurringContributionStartDate = null, DateTimeOffset? recurringContributionEndDate = null, bool? isCloseable = null,
            Connection connection = null, object userDefinedObjectForLogging = null, string externalProgramTag = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, accountId, metaData);
            a.Name = name;
            a.TargetAmount = targetAmount;
            a.TargetDate = targetDate;
            a.Category = category;
            a.SubCategory = subCategory;
            a.Tag = tag;
            a.Type = type;
            a.RecurringContributionType = recurringContributionType;
            a.RecurringContributionAmount = recurringContributionAmount;
            a.RecurringContributionStartDate = recurringContributionStartDate;
            a.RecurringContributionFromExternalAccountId = recurringContributionFromExternalAccountId;
            a.RecurringContributionEndDate = recurringContributionEndDate;
            a.IsCloseable = isCloseable;
            a.ExternalProgramTag = externalProgramTag;
            return await a.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Account> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Account>(cancellationToken, "account/create", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
            {
                this.RequestId = rv.RequestId;
                if (rv.Data != null)
                {
                    this.AccountId = (int)rv.Data.AccountId;
                }
            }
            return rv.Data;
        }

        public async static Task<AccountClose> CloseAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? closeToAccountId, string transactionTag, string closeReason, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return await new Account(customerId, accountId, metaData).CloseAsync(cancellationToken, closeToAccountId, transactionTag, closeReason, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<AccountClose> CloseAsync(CancellationToken cancellationToken, int? closeToAccountId, string transactionTag, string closeReason, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await AccountClose.CloseAsync(cancellationToken, this.CustomerId, this.AccountId, closeToAccountId, transactionTag, closeReason, connection, userDefinedObjectForLogging, metaData);
            return rv;
        }






        public static async Task<AccountAccess> AddAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, string accessTypeCode = null, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.AccessTypeCode = accessTypeCode;
            cal.CustomerPriority = customerPriority;
            return await cal.AddAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<AccountAccess> AddAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(this.CustomerId, this.AccountId, this.MetaData);
            cal.AccessTypeCode = this.AccessTypeCode;
            cal.CustomerPriority = customerPriority;
            return await cal.AddAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        public static async Task<AccountAccess> EditAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, string accessTypeCode = null, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.AccessTypeCode = accessTypeCode;
            cal.CustomerPriority = customerPriority;
            return await cal.EditAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<AccountAccess> EditAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(this.CustomerId, this.AccountId, this.MetaData);
            cal.AccessTypeCode = this.AccessTypeCode;
            cal.CustomerPriority = customerPriority;
            return await cal.EditAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public static async Task<AccountAccess> RemoveAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            return await cal.RemoveAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<AccountAccess> RemoveAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(this.CustomerId, this.AccountId, this.MetaData);
            return await cal.RemoveAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public static async Task<List<AccountAccess>> ListAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            return (await cal.ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }
        public virtual async Task<List<AccountAccess>> ListAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(this.CustomerId, this.AccountId, this.MetaData);
            return (await cal.ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public static async Task<Envelope<List<AccountAccess>>> ListAccessAsyncEnvelope(CancellationToken cancellationToken, int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            return await cal.ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public virtual async Task<Envelope<List<AccountAccess>>> ListAccessAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(this.CustomerId, this.AccountId, this.MetaData);
            return await cal.ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        #endregion Async


        #region Synchronous

        public static List<Account> List(int? customerId, int? accountId = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new Account(customerId, accountId, metaData).List(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual List<Account> List(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<Account>>(String.Format("account/list/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static Account Get(int? customerId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new Account(customerId, accountId, metaData).Get(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Account Get(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Account>(String.Format("account/get/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static Account GetByTag(int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, null, metaData);
            a.Tag = tag;
            return a.GetByTag(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Account GetByTag(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Account>(String.Format("account/getbytag/{0}/?tag={1}", this.CustomerId, Uri.EscapeDataString(this.Tag + "")), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }


        public static bool Update(int? customerId, int? accountId, string name = null, string tag = null, 
            string category = null, string subCategory = null, string miscellaneous = null /* no longer used, remains only to prevent breaking interface */ ,
            decimal? targetAmount = null, DateTimeOffset? targetDate = null, 
            string recurringContributionType = null, decimal? recurringContributionAmount = null, int? recurringContributionFromExternalAccountId = null,
            DateTimeOffset? recurringContributionStartDate = null, DateTimeOffset? recurringContributionEndDate = null, bool? isCloseable = null,
            Connection connection = null, object userDefinedObjectForLogging = null, string externalProgramTag = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, accountId, metaData);
            a.Name = name;
            a.TargetAmount = targetAmount;
            a.TargetDate = targetDate;
            a.Category = category;
            a.SubCategory = subCategory;
            a.Tag = tag;
            a.RecurringContributionType = recurringContributionType;
            a.RecurringContributionAmount = recurringContributionAmount;
            a.RecurringContributionStartDate = recurringContributionStartDate;
            a.RecurringContributionFromExternalAccountId = recurringContributionFromExternalAccountId;
            a.RecurringContributionEndDate = recurringContributionEndDate;
            a.IsCloseable = isCloseable;
            a.ExternalProgramTag = externalProgramTag;
            return a.Update(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual bool Update(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Account>("account/update", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        public static int Create(int? customerId, int? accountId, string name, string type, string tag = null,
            string category = null, string subCategory = null, string miscellaneous = null /* no longer used, remains only to prevent breaking interface */ ,
            decimal? targetAmount = null, DateTimeOffset? targetDate = null,
            string recurringContributionType = null, decimal? recurringContributionAmount = null, int? recurringContributionFromExternalAccountId = null,
            DateTimeOffset? recurringContributionStartDate = null, DateTimeOffset? recurringContributionEndDate = null, bool? isCloseable = null,
            Connection connection = null, object userDefinedObjectForLogging = null, string externalProgramTag = null, RequestMetaData metaData = null)
        {
            var a = new Account(customerId, accountId, metaData);
            a.Name = name;
            a.TargetAmount = targetAmount;
            a.TargetDate = targetDate;
            a.Category = category;
            a.SubCategory = subCategory;
            a.Tag = tag;
            a.Type = type;
            a.RecurringContributionType = recurringContributionType;
            a.RecurringContributionAmount = recurringContributionAmount;
            a.RecurringContributionStartDate = recurringContributionStartDate;
            a.RecurringContributionFromExternalAccountId = recurringContributionFromExternalAccountId;
            a.RecurringContributionEndDate = recurringContributionEndDate;
            a.IsCloseable = isCloseable;
            a.ExternalProgramTag = externalProgramTag;
            return a.Create(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual int Create(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Account>("account/create", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
            {
                this.AccountId = (int)rv.AccountId;
                this.RequestId = rv.RequestId;
            }
            return (int)this.AccountId;
        }

        public static AccountClose Close(int? customerId, int? accountId, int? closeToAccountId, string transactionTag, string closeReason, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new Account(customerId, accountId, metaData).Close(closeToAccountId, transactionTag, closeReason, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual AccountClose Close(int? closeToAccountId, string transactionTag, string closeReason, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = AccountClose.Close(this.CustomerId, this.AccountId, closeToAccountId, transactionTag, closeReason, connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }






        public static AccountAccess AddAccess(int? customerId, int? accountId, int? targetCustomerId, string accessTypeCode = null, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            return AccountAccess.AddAccess(customerId, accountId, targetCustomerId, accessTypeCode, connection, userDefinedObjectForLogging, customerPriority, metaData);
        }

        public virtual AccountAccess AddAccess(int? targetCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            return AccountAccess.AddAccess(this.CustomerId, this.AccountId, targetCustomerId, this.AccessTypeCode, connection, userDefinedObjectForLogging, customerPriority, metaData ?? this.MetaData);
        }


        public static AccountAccess EditAccess(int? customerId, int? accountId, int? targetCustomerId, string accessTypeCode = null, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            return AccountAccess.EditAccess(customerId, accountId, targetCustomerId, accessTypeCode, connection, userDefinedObjectForLogging, customerPriority, metaData);
        }

        public virtual AccountAccess EditAccess(int? targetCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            return AccountAccess.EditAccess(this.CustomerId, this.AccountId, targetCustomerId, this.AccessTypeCode, connection, userDefinedObjectForLogging, customerPriority, metaData ?? this.MetaData);
        }

        public static AccountAccess RemoveAccess(int? customerId, int? accountId, int? targetCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return AccountAccess.RemoveAccess(customerId, accountId, targetCustomerId, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual AccountAccess RemoveAccess(int? targetCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return AccountAccess.RemoveAccess(this.CustomerId, this.AccountId, targetCustomerId, connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
        }


        public virtual List<AccountAccess> ListAccess(int? targetCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return AccountAccess.ListAccess(this.CustomerId, this.AccountId, targetCustomerId, connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
        }

        #endregion Synchronous

    }
}
