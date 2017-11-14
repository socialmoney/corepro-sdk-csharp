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
    public class CustomerReferral : ModelBase
    {
        public CustomerReferral() : base()
        {
        }

        public CustomerReferral(RequestMetaData metaData) : base(metaData)
        {

        }
        public CustomerReferral(int? customerId, RequestMetaData metaData = null)
            : this(metaData)
        {
            this.CustomerId = customerId;
        }
        public CustomerReferral(int? customerId, int? referralCustomerId, RequestMetaData metaData = null)
            : this(customerId, metaData)
        {
            this.ReferralCustomerId = referralCustomerId;
        }

        public int? CustomerId { get; set; }
        public int? ReferralCustomerId { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string Tag { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        #region Synchronous
        public static List<CustomerReferral> List(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new CustomerReferral(customerId, metaData).List(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual List<CustomerReferral> List(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<CustomerReferral>>(String.Format("customerReferral/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static CustomerReferral Get(int? customerId, int? referralCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerReferral(customerId, metaData);
            cb.ReferralCustomerId = referralCustomerId;
            return cb.Get(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual CustomerReferral Get(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<CustomerReferral>(String.Format("customerReferral/get/{0}/{1}", this.CustomerId, this.ReferralCustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static CustomerReferral Create(int? customerId, int? referralCustomerId, string tag = null, string customField1 = null, string customField2 = null, string customField3 = null, string addressLine4 = null, 
                string customField4 = null, string customField5 = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerReferral(customerId, metaData);
            cb.ReferralCustomerId = referralCustomerId;
            cb.Tag = tag;
            cb.CustomField1 = customField1;
            cb.CustomField2 = customField2;
            cb.CustomField3 = customField3;
            cb.CustomField4 = customField4;
            cb.CustomField5 = customField5;

            return cb.Create(connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer referral using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerReferral Create(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomerReferral = Requestor.Post<CustomerReferral>("customerReferral/create", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = newCustomerReferral.RequestId;
            var rv = CustomerReferral.Get(this.CustomerId, (int)newCustomerReferral.ReferralCustomerId, connection, userDefinedObjectForLogging, metaData);
            rv.RequestId = newCustomerReferral.RequestId;
            return rv;
        }

        #endregion Synchronous

        #region Async
        public async static Task<List<CustomerReferral>> ListAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return await new CustomerReferral(customerId, metaData).ListAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<List<CustomerReferral>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<CustomerReferral>>(cancellationToken, String.Format("customerReferral/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv.Data;
        }

        public async static Task<CustomerReferral> GetAsync(CancellationToken cancellationToken, int? customerId, int? referralCustomerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerReferral(customerId, metaData);
            cb.ReferralCustomerId = referralCustomerId;
            return await cb.GetAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<CustomerReferral> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<CustomerReferral>(cancellationToken, String.Format("customerReferral/get/{0}/{1}", this.CustomerId, this.ReferralCustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv.Data;
        }

        public async static Task<CustomerReferral> CreateAsync(CancellationToken cancellationToken, int? customerId, int? referralCustomerId, string tag = null, 
                string customField1 = null, string customField2 = null, string customField3 = null, string addressLine4 = null, string customField4 = null, string customField5 = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerReferral(customerId, metaData);
            cb.ReferralCustomerId = referralCustomerId;
            cb.Tag = tag;
            cb.CustomField1 = customField1;
            cb.CustomField2 = customField2;
            cb.CustomField3 = customField3;
            cb.CustomField4 = customField4;
            cb.CustomField5 = customField5;

            return await cb.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer referral using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerReferral> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomerReferral = (await Requestor.PostAsync<CustomerReferral>(cancellationToken, "customerReferral/create", connection, this, userDefinedObjectForLogging, metaData)).Data;
            this.RequestId = newCustomerReferral.RequestId;
            var rv = await CustomerReferral.GetAsync(cancellationToken, this.CustomerId, (int)newCustomerReferral.ReferralCustomerId, connection, userDefinedObjectForLogging, metaData);
            rv.RequestId = newCustomerReferral.RequestId;
            return rv;
        }


        #endregion Async

    }
}
