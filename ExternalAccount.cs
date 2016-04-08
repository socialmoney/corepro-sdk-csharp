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
    public class ExternalAccount : ModelBase
    {

        public ExternalAccount()
        {

        }

        public ExternalAccount(int? customerId, int? externalAccountId)
            : this()
        {
            this.CustomerId = customerId;
            this.ExternalAccountId = externalAccountId;
        }

        public int? CustomerId { get; set; }
        public int? ExternalAccountId { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? StatusDate { get; set; }
        public string RoutingNumber { get; set; }
        public string RoutingNumberMasked { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNumberMasked { get; set; }
        public string NOCCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public DateTimeOffset? LockedDate { get; set; }
        public string LockedReason { get; set; }

        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }

        public DateTimeOffset? LastVerifySentDate { get; set; }
        public DateTimeOffset? LastVerifyExpiredDate { get; set; }

        #region Synchronous

        public static List<ExternalAccount> List(int? customerId, int? externalAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new ExternalAccount(customerId, externalAccountId).List(connection, userDefinedObjectForLogging);
        }

        public virtual List<ExternalAccount> List(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<ExternalAccount>>(String.Format("externalaccount/list/{0}/{1}", this.CustomerId, this.ExternalAccountId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static ExternalAccount Get(int? customerId, int? externalAccountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new ExternalAccount(customerId, externalAccountId).Get(connection, userDefinedObjectForLogging);
        }

        public virtual ExternalAccount Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<ExternalAccount>(String.Format("externalaccount/get/{0}/{1}", this.CustomerId, this.ExternalAccountId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static ExternalAccount GetByTag(int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Tag = tag;
            return ea.GetByTag(connection, userDefinedObjectForLogging);
        }

        public virtual ExternalAccount GetByTag(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<ExternalAccount>(String.Format("externalaccount/getbytag/{0}/{1}", this.CustomerId, this.Tag), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static bool Verify(int? customerId, int? externalAccountId, decimal amount1, decimal amount2, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            return ea.Verify(amount1, amount2, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses given parameters (or if null, corresponding properties on the current object) to verify the external account.  
        /// If successful, sets status to 'Verified' and returns true.  
        /// If failed too many times, will cause status to be set to 'Denied' (i.e. verify attempts has been exceeded -- see message text for # of attempts remaining).
        /// Also, status can be 'VerifyLocked' meaning an administrator locked the verification process, and this will fail.
        /// In failure results in this method throwing an exception with a code and descriptive reason as to why it failed.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="amount1"></param>
        /// <param name="amount2"></param>
        /// <param name="connection"></param>
        public virtual bool Verify(decimal amount1, decimal amount2, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, ExternalAccountId = this.ExternalAccountId, Amount1 = amount1, Amount2 = amount2 };
            var rv = Requestor.Post<ModelBase>("externalaccount/verify", connection, body, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        public static bool Archive(int? customerId, int? externalAccountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            return ea.Archive(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses given parameters (or if null, corresponding properties on the current object) to deactivate the external account.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Archive(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, ExternalAccountId = this.ExternalAccountId };
            var rv1 = Requestor.Post<ModelBase>("externalaccount/archive", connection, body, userDefinedObjectForLogging);
            this.RequestId = rv1.RequestId;
            return true;
        }

        public static bool Update(int? customerId, int? externalAccountId, string nickName = null, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            ea.NickName = nickName;
            ea.Tag = tag;
            return ea.Update(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses properties on current object to update the external account information.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Update(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<ModelBase>("externalaccount/update", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        public static int Create(int? customerId, string name, string firstName, string lastName, string type,
            string routingNumber, string accountNumber, string nickName = null, string tag = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Name = name;
            ea.FirstName = firstName;
            ea.LastName = lastName;
            ea.NickName = nickName;
            ea.Type = type;
            ea.Tag = tag;
            ea.AccountNumber = accountNumber;
            ea.RoutingNumber = routingNumber;
            return ea.Create(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates an external account using properties on the current object.  Note it will be in a status of 'Unverified' until Verify is called unless your program is configured to allow external accounts to be auto-verified.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual int Create(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<ExternalAccount>("externalaccount/create", connection, this, userDefinedObjectForLogging);
            if (rv != null)
            {
                this.ExternalAccountId = (int)rv.ExternalAccountId;
                this.RequestId = rv.RequestId;
            }
            return (int)this.ExternalAccountId;
        }

        public static int Initiate(int? customerId, string name, string firstName, string lastName, string type,
            string routingNumber, string accountNumber, string nickName = null, string tag = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Name = name;
            ea.FirstName = firstName;
            ea.LastName = lastName;
            ea.NickName = nickName;
            ea.Type = type;
            ea.Tag = tag;
            ea.AccountNumber = accountNumber;
            ea.RoutingNumber = routingNumber;
            return ea.Initiate(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates an external account using properties on the current object.  Note it will be in a status of 'Unverified' until Verify is called unless your program is configured to allow external accounts to be auto-verified.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual int Initiate(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<ExternalAccount>("externalaccount/initiate", connection, this, userDefinedObjectForLogging);
            if (rv != null)
            {
                this.ExternalAccountId = (int)rv.ExternalAccountId;
                this.RequestId = rv.RequestId;
            }
            return (int)this.ExternalAccountId;
        }


        #endregion Synchronous










        #region Async
        public async static Task<List<ExternalAccount>> ListAsync(CancellationToken cancellationToken, int? customerId, int? externalAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new ExternalAccount(customerId, externalAccountId).ListAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<ExternalAccount>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<ExternalAccount>>(cancellationToken, String.Format("externalaccount/list/{0}/{1}", this.CustomerId, this.ExternalAccountId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<ExternalAccount> GetAsync(CancellationToken cancellationToken, int? customerId, int? externalAccountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new ExternalAccount(customerId, externalAccountId).GetAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<ExternalAccount> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<ExternalAccount>(cancellationToken, String.Format("externalaccount/get/{0}/{1}", this.CustomerId, this.ExternalAccountId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<ExternalAccount> GetByTagAsync(CancellationToken cancellationToken, int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Tag = tag;
            return await ea.GetByTagAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<ExternalAccount> GetByTagAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<ExternalAccount>(cancellationToken, String.Format("externalaccount/getbytag/{0}/{1}", this.CustomerId, this.Tag), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<bool> VerifyAsync(CancellationToken cancellationToken, int? customerId, int? externalAccountId, decimal amount1, decimal amount2, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            return await ea.VerifyAsync(cancellationToken, amount1, amount2, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses given parameters (or if null, corresponding properties on the current object) to verify the external account.  
        /// If successful, sets status to 'Verified' and returns true.  
        /// If failed too many times, will cause status to be set to 'Denied' (i.e. verify attempts has been exceeded -- see message text for # of attempts remaining).
        /// Also, status can be 'VerifyLocked' meaning an administrator locked the verification process, and this will fail.
        /// In failure results in this method throwing an exception with a code and descriptive reason as to why it failed.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="amount1"></param>
        /// <param name="amount2"></param>
        /// <param name="connection"></param>
        public async virtual Task<bool> VerifyAsync(CancellationToken cancellationToken, decimal amount1, decimal amount2, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, ExternalAccountId = this.ExternalAccountId, Amount1 = amount1, Amount2 = amount2 };
            var rv = await Requestor.PostAsync<ModelBase>(cancellationToken, "externalaccount/verify", connection, body, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        public async static Task<bool> ArchiveAsync(CancellationToken cancellationToken, int? customerId, int? externalAccountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            return await ea.ArchiveAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses given parameters (or if null, corresponding properties on the current object) to deactivate the external account.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<bool> ArchiveAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, ExternalAccountId = this.ExternalAccountId };
            var rv1 = await Requestor.PostAsync<ModelBase>(cancellationToken, "externalaccount/archive", connection, body, userDefinedObjectForLogging);
            this.RequestId = rv1.RequestId;
            return true;
        }

        public async static Task<bool> UpdateAsync(CancellationToken cancellationToken, int? customerId, int? externalAccountId, string nickName = null, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            ea.NickName = nickName;
            ea.Tag = tag;
            return await ea.UpdateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses properties on current object to update the external account information.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<bool> UpdateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<ModelBase>(cancellationToken, "externalaccount/update", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        public async static Task<int> CreateAsync(CancellationToken cancellationToken, int? customerId, string name, string firstName, string lastName, string type,
            string routingNumber, string accountNumber, string nickName = null, string tag = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Name = name;
            ea.FirstName = firstName;
            ea.LastName = lastName;
            ea.NickName = nickName;
            ea.Type = type;
            ea.Tag = tag;
            ea.AccountNumber = accountNumber;
            ea.RoutingNumber = routingNumber;
            return await ea.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates an external account using properties on the current object.  Note it will be in a status of 'Unverified' until Verify is called unless your program is configured to allow external accounts to be auto-verified.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<int> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<ExternalAccount>(cancellationToken, "externalaccount/create", connection, this, userDefinedObjectForLogging);
            if (rv != null)
            {
                this.ExternalAccountId = (int)rv.Data.ExternalAccountId;
                this.RequestId = rv.RequestId;
            }
            return (int)this.ExternalAccountId;
        }

        public async static Task<int> InitiateAsync(CancellationToken cancellationToken, int? customerId, string name, string firstName, string lastName, string type,
            string routingNumber, string accountNumber, string nickName = null, string tag = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Name = name;
            ea.FirstName = firstName;
            ea.LastName = lastName;
            ea.NickName = nickName;
            ea.Type = type;
            ea.Tag = tag;
            ea.AccountNumber = accountNumber;
            ea.RoutingNumber = routingNumber;
            return await ea.InitiateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates an external account using properties on the current object.  Note it will be in a status of 'Unverified' until Verify is called unless your program is configured to allow external accounts to be auto-verified.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<int> InitiateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<ExternalAccount>(cancellationToken, "externalaccount/initiate", connection, this, userDefinedObjectForLogging);
            if (rv != null)
            {
                this.ExternalAccountId = (int)rv.Data.ExternalAccountId;
                this.RequestId = rv.RequestId;
            }
            return (int)this.ExternalAccountId;
        }
        #endregion Async

    }
}
