using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var rv = CustomerBeneficiary.Get(this.CustomerId, (int)newCustomerBeneficiary.CustomerBeneficiaryId, connection, userDefinedObjectForLogging);
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
            Requestor.Post<dynamic>("customerBeneficiary/update", connection, this, userDefinedObjectForLogging);
            var rv = CustomerBeneficiary.Get(this.CustomerId, (int)this.CustomerBeneficiaryId, connection, userDefinedObjectForLogging);
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
            Requestor.Post<object>("customerBeneficiary/deactivate", connection, body, userDefinedObjectForLogging);
            return true;
        }


    }
}
