using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class AccountAccess : ModelBase
    {
        public AccountAccess() : base()
        {

        }

        public AccountAccess(RequestMetaData metaData) : base(metaData)
        {

        }

        public AccountAccess(int? customerId, int? accountId, RequestMetaData metaData = null) : base(metaData)
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        public int? TargetCustomerId { get; set;  }

        public int? CustomerId { get; set; }
        public string CustomerTag { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string EmailAddress { get; set; }


        public int? AccountId { get; set; }

        public string AccountTag { get; set; }
        public string RoutingNumberMasked { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumberMasked { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccessTypeCode { get; set; }

        public int? PrimaryCustomerId { get; set; }
        public bool? IsPrimaryCustomer { get; set; }

        public int? CustomerPriority { get; set; }

        #region Asynchronous

        public static async Task<AccountAccess> AddAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? targetCustomerId, string accessTypeCode, 
            Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.AccessTypeCode = accessTypeCode;
            cal.TargetCustomerId = targetCustomerId;
            cal.CustomerPriority = customerPriority;
            return await cal.AddAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<AccountAccess> AddAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<AccountAccess>(cancellationToken, "account/addAccess", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }


        public static async Task<AccountAccess> EditAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? targetCustomerId, string accessTypeCode, 
            Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.AccessTypeCode = accessTypeCode;
            cal.TargetCustomerId = targetCustomerId;
            cal.CustomerPriority = customerPriority;
            return await cal.EditAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<AccountAccess> EditAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<AccountAccess>(cancellationToken, "account/editAccess", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public static async Task<AccountAccess> RemoveAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? targetCustomerId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.TargetCustomerId = targetCustomerId;
            return await cal.RemoveAccessAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<AccountAccess> RemoveAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<AccountAccess>(cancellationToken, "account/removeAccess", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public static async Task<List<AccountAccess>> ListAccessAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? targetCustomerId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.TargetCustomerId = targetCustomerId;
            return (await cal.ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }
        public virtual async Task<List<AccountAccess>> ListAccessAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public static async Task<Envelope<List<AccountAccess>>> ListAccessAsyncEnvelope(CancellationToken cancellationToken, int? customerId, int? accountId, int? targetCustomerId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.TargetCustomerId = targetCustomerId;
            return await cal.ListAccessAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public virtual async Task<Envelope<List<AccountAccess>>> ListAccessAsyncEnvelope(CancellationToken cancellationToken, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<AccountAccess>>(cancellationToken, String.Format("account/listAccess/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        #endregion Asynchronous


        #region Synchronous

        public static AccountAccess AddAccess(int? customerId, int? accountId, int? targetCustomerId, string accessTypeCode = null, 
            Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.AccessTypeCode = accessTypeCode;
            cal.TargetCustomerId = targetCustomerId;
            cal.CustomerPriority = customerPriority;
            return cal.AddAccess(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual AccountAccess AddAccess(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<AccountAccess>("account/addAccess", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }


        public static AccountAccess EditAccess(int? customerId, int? accountId, int? targetCustomerId, string accessTypeCode, 
            Connection connection = null, object userDefinedObjectForLogging = null, int? customerPriority = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.AccessTypeCode = accessTypeCode;
            cal.TargetCustomerId = targetCustomerId;
            cal.CustomerPriority = customerPriority;
            return cal.EditAccess(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual AccountAccess EditAccess(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<AccountAccess>("account/editAccess", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static AccountAccess RemoveAccess(int? customerId, int? accountId, int? targetCustomerId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.TargetCustomerId = targetCustomerId;
            return cal.RemoveAccess(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual AccountAccess RemoveAccess(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<AccountAccess>("account/removeAccess", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static List<AccountAccess> ListAccess(int? customerId, int? accountId, int? targetCustomerId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cal = new AccountAccess(customerId, accountId, metaData);
            cal.TargetCustomerId = targetCustomerId;
            return cal.ListAccess(connection, userDefinedObjectForLogging);
        }

        public virtual List<AccountAccess> ListAccess(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<AccountAccess>>(String.Format("account/listAccess/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        #endregion Synchronous

    }
}
