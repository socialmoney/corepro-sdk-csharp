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
    public class CustomerBeneficiary : ModelBase
    {
        public CustomerBeneficiary()
        {

        }
        public CustomerBeneficiary(int? customerId)
            : this()
        {
            this.CustomerId = customerId;
        }
        public CustomerBeneficiary(int? customerId, int? customerBeneficiaryId)
            : this(customerId)
        {
            this.CustomerBeneficiaryId = customerBeneficiaryId;
        }

        public int? CustomerId { get; set; }
        public int? CustomerBeneficiaryId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string TaxId { get; set; }
        public string TaxIdMasked { get; set; }
        public bool? IsActive { get; set; }

        #region Synchronous
        public static List<CustomerBeneficiary> List(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new CustomerBeneficiary(customerId).List(connection, userDefinedObjectForLogging);
        }

        public virtual List<CustomerBeneficiary> List(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<CustomerBeneficiary>>(String.Format("customerBeneficiary/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerBeneficiary Get(int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            return cb.Get(connection, userDefinedObjectForLogging);
        }

        public virtual CustomerBeneficiary Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<CustomerBeneficiary>(String.Format("customerBeneficiary/get/{0}/{1}", this.CustomerId, this.CustomerBeneficiaryId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerBeneficiary Create(int? customerId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, 
                Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId);
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            return cb.Create(connection, userDefinedObjectForLogging);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerBeneficiary Create(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomerBeneficiary = Requestor.Post<CustomerBeneficiary>("customerBeneficiary/create", connection, this, userDefinedObjectForLogging);
            this.RequestId = newCustomerBeneficiary.RequestId;
            var rv = CustomerBeneficiary.Get(this.CustomerId, (int)newCustomerBeneficiary.CustomerBeneficiaryId, connection, userDefinedObjectForLogging);
            rv.RequestId = newCustomerBeneficiary.RequestId;
            return rv;
        }

        public static CustomerBeneficiary Update(int? customerId, int? customerBeneficiaryId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null,
                Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            return cb.Update(connection, userDefinedObjectForLogging);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerBeneficiary Update(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv1 = Requestor.Post<CustomerBeneficiary>("customerBeneficiary/update", connection, this, userDefinedObjectForLogging);
            this.RequestId = rv1.RequestId;
            var rv = CustomerBeneficiary.Get(this.CustomerId, (int)this.CustomerBeneficiaryId, connection, userDefinedObjectForLogging);
            rv.RequestId = rv1.RequestId;
            return rv;
        }


        public static bool Deactivate(int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId, customerBeneficiaryId);
            return cb.Deactivate(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses properties on the current object to deactivate the customer beneficiary.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Deactivate(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, CustomerBeneficiaryId = this.CustomerBeneficiaryId };
            var rv = Requestor.Post<dynamic>("customerBeneficiary/deactivate", connection, body, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        #endregion Synchronous

        #region Async
        public async static Task<List<CustomerBeneficiary>> ListAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new CustomerBeneficiary(customerId).ListAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<CustomerBeneficiary>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<CustomerBeneficiary>>(cancellationToken, String.Format("customerBeneficiary/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<CustomerBeneficiary> GetAsync(CancellationToken cancellationToken, int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            return await cb.GetAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<CustomerBeneficiary> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<CustomerBeneficiary>(cancellationToken, String.Format("customerBeneficiary/get/{0}/{1}", this.CustomerId, this.CustomerBeneficiaryId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<CustomerBeneficiary> CreateAsync(CancellationToken cancellationToken, int? customerId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null,
                Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId);
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            return await cb.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerBeneficiary> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomerBeneficiary = (await Requestor.PostAsync<CustomerBeneficiary>(cancellationToken, "customerBeneficiary/create", connection, this, userDefinedObjectForLogging)).Data;
            this.RequestId = newCustomerBeneficiary.RequestId;
            var rv = await CustomerBeneficiary.GetAsync(cancellationToken, this.CustomerId, (int)newCustomerBeneficiary.CustomerBeneficiaryId, connection, userDefinedObjectForLogging);
            rv.RequestId = newCustomerBeneficiary.RequestId;
            return rv;
        }

        public async static Task<CustomerBeneficiary> UpdateAsync(CancellationToken cancellationToken, int? customerId, int? customerBeneficiaryId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null,
                Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            return await cb.UpdateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerBeneficiary> UpdateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv1 = await Requestor.PostAsync<CustomerBeneficiary>(cancellationToken, "customerBeneficiary/update", connection, this, userDefinedObjectForLogging);
            this.RequestId = rv1.RequestId;
            var rv = await CustomerBeneficiary.GetAsync(cancellationToken, this.CustomerId, (int)this.CustomerBeneficiaryId, connection, userDefinedObjectForLogging);
            rv.RequestId = rv1.RequestId;
            return rv;
        }


        public async static Task<bool> DeactivateAsync(CancellationToken cancellationToken, int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var cb = new CustomerBeneficiary(customerId, customerBeneficiaryId);
            return await cb.DeactivateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses properties on the current object to deactivate the customer beneficiary.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<bool> DeactivateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, CustomerBeneficiaryId = this.CustomerBeneficiaryId };
            var rv = await Requestor.PostAsync<dynamic>(cancellationToken, "customerBeneficiary/deactivate", connection, body, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }


        #endregion Async

    }
}
