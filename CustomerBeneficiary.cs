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
        public CustomerBeneficiary() : base()
        {
        }

        public CustomerBeneficiary(RequestMetaData metaData) : base(metaData)
        {

        }
        public CustomerBeneficiary(int? customerId, RequestMetaData metaData = null)
            : this(metaData)
        {
            this.CustomerId = customerId;
        }
        public CustomerBeneficiary(int? customerId, int? customerBeneficiaryId, RequestMetaData metaData = null)
            : this(customerId, metaData)
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
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LegalName1 { get; set; }
        public string LegalName2 { get; set; }

        public string DistributionDescription { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }


        #region Synchronous
        public static List<CustomerBeneficiary> List(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new CustomerBeneficiary(customerId, metaData).List(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual List<CustomerBeneficiary> List(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<CustomerBeneficiary>>(String.Format("customerBeneficiary/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static CustomerBeneficiary Get(int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, metaData);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            return cb.Get(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual CustomerBeneficiary Get(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<CustomerBeneficiary>(String.Format("customerBeneficiary/get/{0}/{1}", this.CustomerId, this.CustomerBeneficiaryId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static CustomerBeneficiary Create(int? customerId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, string distributionDescription = null,
                string addressLine1 = null, string addressLine2 = null, string addressLine3 = null, string addressLine4 = null, string city = null, string state = null, string postalCode = null, string country = null, string legalName1 = null, string legalName2 = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, metaData);
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            cb.DistributionDescription = distributionDescription;
            cb.AddressLine1 = addressLine1;
            cb.AddressLine2 = addressLine2;
            cb.AddressLine3 = addressLine3;
            cb.AddressLine4 = addressLine4;
            cb.City = city;
            cb.State = state;
            cb.PostalCode = postalCode;
            cb.Country = country;
            cb.LegalName1 = legalName1;
            cb.LegalName2 = legalName2;
            return cb.Create(connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerBeneficiary Create(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomerBeneficiary = Requestor.Post<CustomerBeneficiary>("customerBeneficiary/create", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = newCustomerBeneficiary.RequestId;
            var rv = CustomerBeneficiary.Get(this.CustomerId, (int)newCustomerBeneficiary.CustomerBeneficiaryId, connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            rv.RequestId = newCustomerBeneficiary.RequestId;
            return rv;
        }

        public static CustomerBeneficiary Update(int? customerId, int? customerBeneficiaryId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, string distributionDescription = null,
                string addressLine1 = null, string addressLine2 = null, string addressLine3 = null, string addressLine4 = null, string city = null, string state = null, string postalCode = null, string country = null, string legalName1 = null, string legalName2 = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, metaData);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            cb.DistributionDescription = distributionDescription;
            cb.AddressLine1 = addressLine1;
            cb.AddressLine2 = addressLine2;
            cb.AddressLine3 = addressLine3;
            cb.AddressLine4 = addressLine4;
            cb.City = city;
            cb.State = state;
            cb.PostalCode = postalCode;
            cb.Country = country;
            cb.LegalName1 = legalName1;
            cb.LegalName2 = legalName2;
            return cb.Update(connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerBeneficiary Update(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv1 = Requestor.Post<CustomerBeneficiary>("customerBeneficiary/update", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = rv1.RequestId;
            var rv = CustomerBeneficiary.Get(this.CustomerId, (int)this.CustomerBeneficiaryId, connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            rv.RequestId = rv1.RequestId;
            return rv;
        }


        public static bool Deactivate(int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, customerBeneficiaryId, metaData);
            return cb.Deactivate(connection, userDefinedObjectForLogging, metaData);
        }

        /// <summary>
        /// Uses properties on the current object to deactivate the customer beneficiary.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Deactivate(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, CustomerBeneficiaryId = this.CustomerBeneficiaryId };
            var rv = Requestor.Post<dynamic>("customerBeneficiary/deactivate", connection, body, userDefinedObjectForLogging, metaData ?? this.MetaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }

        #endregion Synchronous

        #region Async
        public async static Task<List<CustomerBeneficiary>> ListAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return await new CustomerBeneficiary(customerId, metaData).ListAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<List<CustomerBeneficiary>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<CustomerBeneficiary>>(cancellationToken, String.Format("customerBeneficiary/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv.Data;
        }

        public async static Task<CustomerBeneficiary> GetAsync(CancellationToken cancellationToken, int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, metaData);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            return await cb.GetAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<CustomerBeneficiary> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<CustomerBeneficiary>(cancellationToken, String.Format("customerBeneficiary/get/{0}/{1}", this.CustomerId, this.CustomerBeneficiaryId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv.Data;
        }

        public async static Task<CustomerBeneficiary> CreateAsync(CancellationToken cancellationToken, int? customerId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, string distributionDescription = null,
                string addressLine1 = null, string addressLine2 = null, string addressLine3 = null, string addressLine4 = null, string city = null, string state = null, string postalCode = null, string country = null, string legalName1 = null, string legalName2 = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, metaData);
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            cb.DistributionDescription = distributionDescription;
            cb.AddressLine1 = addressLine1;
            cb.AddressLine2 = addressLine2;
            cb.AddressLine3 = addressLine3;
            cb.AddressLine4 = addressLine4;
            cb.City = city;
            cb.State = state;
            cb.PostalCode = postalCode;
            cb.Country = country;
            cb.LegalName1 = legalName1;
            cb.LegalName2 = legalName2;
            return await cb.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerBeneficiary> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomerBeneficiary = (await Requestor.PostAsync<CustomerBeneficiary>(cancellationToken, "customerBeneficiary/create", connection, this, userDefinedObjectForLogging, metaData)).Data;
            this.RequestId = newCustomerBeneficiary.RequestId;
            var rv = await CustomerBeneficiary.GetAsync(cancellationToken, this.CustomerId, (int)newCustomerBeneficiary.CustomerBeneficiaryId, connection, userDefinedObjectForLogging, metaData);
            rv.RequestId = newCustomerBeneficiary.RequestId;
            return rv;
        }

        public async static Task<CustomerBeneficiary> UpdateAsync(CancellationToken cancellationToken, int? customerId, int? customerBeneficiaryId, string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, string distributionDescription = null,
                string addressLine1 = null, string addressLine2 = null, string addressLine3 = null, string addressLine4 = null, string city = null, string state = null, string postalCode = null, string country = null, string legalName1 = null, string legalName2 = null,
                Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, metaData);
            cb.CustomerBeneficiaryId = customerBeneficiaryId;
            cb.FirstName = firstName;
            cb.MiddleName = middleName;
            cb.LastName = lastName;
            cb.TaxId = taxId;
            cb.BirthDate = birthDate;
            cb.DistributionDescription = distributionDescription;
            cb.AddressLine1 = addressLine1;
            cb.AddressLine2 = addressLine2;
            cb.AddressLine3 = addressLine3;
            cb.AddressLine4 = addressLine4;
            cb.City = city;
            cb.State = state;
            cb.PostalCode = postalCode;
            cb.Country = country;
            cb.LegalName1 = legalName1;
            cb.LegalName2 = legalName2;
            return await cb.UpdateAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        /// <summary>
        /// Creates a customer beneficiary using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerBeneficiary> UpdateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv1 = await Requestor.PostAsync<CustomerBeneficiary>(cancellationToken, "customerBeneficiary/update", connection, this, userDefinedObjectForLogging, metaData);
            this.RequestId = rv1.RequestId;
            var rv = await CustomerBeneficiary.GetAsync(cancellationToken, this.CustomerId, (int)this.CustomerBeneficiaryId, connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            rv.RequestId = rv1.RequestId;
            return rv;
        }


        public async static Task<bool> DeactivateAsync(CancellationToken cancellationToken, int? customerId, int? customerBeneficiaryId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cb = new CustomerBeneficiary(customerId, customerBeneficiaryId, metaData);
            return await cb.DeactivateAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        /// <summary>
        /// Uses properties on the current object to deactivate the customer beneficiary.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<bool> DeactivateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, CustomerBeneficiaryId = this.CustomerBeneficiaryId };
            var rv = await Requestor.PostAsync<dynamic>(cancellationToken, "customerBeneficiary/deactivate", connection, body, userDefinedObjectForLogging, metaData ?? this.MetaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return true;
        }


        #endregion Async

    }
}
