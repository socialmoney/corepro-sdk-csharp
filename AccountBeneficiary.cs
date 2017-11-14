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
    public class AccountBeneficiary : ModelBase
    {
        public AccountBeneficiary() : base()
        {

        }
        public AccountBeneficiary(RequestMetaData metaData) : base(metaData)
        {

        }

        public AccountBeneficiary(int? customerId, RequestMetaData metaData = null)
            : base(metaData)
        {
            this.CustomerId = customerId;
        }

        public AccountBeneficiary(int? customerId, int? accountId, RequestMetaData metaData = null) 
            : base(metaData)
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }
        public AccountBeneficiary(int? customerId, int? accountId, int? customerBeneficiaryId, RequestMetaData metaData = null) : base(metaData)
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
            this.CustomerBeneficiaryId = customerBeneficiaryId;
        }

        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public int? CustomerBeneficiaryId { get; set; }

        public string AccountNumber { get; set; }
        public string AccountNumberMasked { get; set; }

        public string AccountName { get; set; }

        public string AccountLegalName1 { get; set; }
        public string AccountLegalName2 { get; set; }

        public string DistributionDescription { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string TaxId { get; set; }
        public string TaxIdMasked { get; set; }
        public bool? IsActive { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        #region Synchronous
        public static List<AccountBeneficiary> List(int? customerId, int? accountId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, metaData);
            ab.AccountId = accountId;
            return ab.List(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual List<AccountBeneficiary> List(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<AccountBeneficiary>>(String.Format("accountBeneficiary/list/{0}/{1}", this.CustomerId, this.AccountId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static List<AccountBeneficiary> Get(int? customerId, int? accountId, int? customerBeneficiaryId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, accountId, customerBeneficiaryId, metaData);
            return ab.Get(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual List<AccountBeneficiary> Get(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<AccountBeneficiary>>(String.Format("accountBeneficiary/get/{0}/{1}", this.CustomerId, this.CustomerBeneficiaryId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static AccountBeneficiary Add(int? customerId, int? accountId, int? customerBeneficiaryId, string distributionDescription = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, metaData);
            ab.AccountId = accountId;
            ab.CustomerBeneficiaryId = customerBeneficiaryId;
            ab.DistributionDescription = distributionDescription;
            return ab.Add(connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Adds a customer beneficiary to the account using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual AccountBeneficiary Add(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<AccountBeneficiary>("accountBeneficiary/add", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = rv.RequestId;
            return rv;
        }

        public static AccountBeneficiary Edit(int? customerId, int? accountId, int? customerBeneficiaryId, string distributionDescription = null, bool? isActive = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, accountId, customerBeneficiaryId, metaData);
            ab.DistributionDescription = distributionDescription;
            return ab.Edit(connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Edits the association between an account and a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual AccountBeneficiary Edit(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<AccountBeneficiary>("accountBeneficiary/edit", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = rv.RequestId;
            return rv;
        }


        public static bool Remove(int? customerId, int? accountId, int? customerBeneficiaryId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, accountId, customerBeneficiaryId, metaData);
            return ab.Remove(connection, userDefinedObjectForLogging, metaData);
        }

        /// <summary>
        /// Uses properties on the current object to deactivate the customer beneficiary.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Remove(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<dynamic>("accountBeneficiary/remove", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        #endregion Synchronous

        #region Async
        public async static Task<List<AccountBeneficiary>> ListAsync(CancellationToken cancellationToken, int? customerId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return await new AccountBeneficiary(customerId, metaData).ListAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<List<AccountBeneficiary>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<AccountBeneficiary>>(cancellationToken, String.Format("accountBeneficiary/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv.Data;
        }

        public async static Task<List<AccountBeneficiary>> GetAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? customerBeneficiaryId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, accountId, customerBeneficiaryId, metaData);
            return await ab.GetAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<List<AccountBeneficiary>> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<AccountBeneficiary>>(cancellationToken, String.Format("accountBeneficiary/get/{0}/{1}", this.CustomerId, this.AccountId, this.CustomerBeneficiaryId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv.Data;
        }

        public async static Task<AccountBeneficiary> AddAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? customerBeneficiaryId, string distributionDescription = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, metaData);
            ab.AccountId = accountId;
            ab.CustomerBeneficiaryId = customerBeneficiaryId;
            ab.DistributionDescription = distributionDescription;
            return await ab.AddAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<AccountBeneficiary> AddAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<AccountBeneficiary>(cancellationToken, "accountBeneficiary/add", connection, this, userDefinedObjectForLogging, metaData ?? this.MetaData);
            this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<AccountBeneficiary> EditAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? customerBeneficiaryId, string distributionDescription = null, bool? isActive = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, accountId, customerBeneficiaryId, metaData);
            ab.DistributionDescription = distributionDescription;
            ab.IsActive = isActive;
            return await ab.EditAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<AccountBeneficiary> EditAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<AccountBeneficiary>(cancellationToken, "accountBeneficiary/edit", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = rv.RequestId;
            return rv.Data;
        }


        public async static Task<bool> RemoveAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? customerBeneficiaryId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ab = new AccountBeneficiary(customerId, accountId, customerBeneficiaryId, metaData);
            return await ab.RemoveAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        /// <summary>
        /// Uses properties on the current object to deactivate the customer beneficiary.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<bool> RemoveAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<dynamic>(cancellationToken, "accountBeneficiary/remove", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }


        #endregion Async


    }
}
