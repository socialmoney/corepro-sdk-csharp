using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class Customer : ModelBase
    {
        public Customer()
        {
            Phones = new List<CustomerPhone>();
            Addresses = new List<CustomerAddress>();
        }

        public Customer(int? customerId)
            : this()
        {
            this.CustomerId = customerId;
        }

        public int? CustomerCount { get; set; }
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Culture { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string TaxId { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseState { get; set; }
        public DateTimeOffset? DriversLicenseExpirationDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportCountry { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public DateTimeOffset? LockedDate { get; set; }
        public string LockedReason { get; set; }
        public bool? IsSubjectToBackupWithholding { get; set; }
        public bool? IsOptedInToBankCommunication { get; set; }
        public bool? IsDocumentsAccepted { get; set; }
        public List<CustomerPhone> Phones { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public DateTimeOffset? DeceasedDate { get; set; }
        public List<Account> Accounts { get; set; }
        public List<ExternalAccount> ExternalAccounts { get; set; }

        public static List<Customer> ListAll(int? pageNumber, int? pageSize, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(0).List(pageNumber, pageSize, connection, userDefinedObjectForLogging);
        }

        public virtual List<Customer> List(int? pageNumber, int? pageSize, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<Customer>>(String.Format("customer/list?pageNumber={0}&pageSize={1}", pageNumber, pageSize), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static Customer Create(string culture, bool isSubjectToBackupWithholding, bool isOptedInToBankCommunication, bool isDocumentsAccepted,
            string tag = null, string gender = "U", string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, string emailAddress = null,
            string driversLicenseNumber = null, string driversLicenseState = null, DateTimeOffset? driversLicenseExpirationDate = null, string passportNumber = null, string passportCountry = null,
            List<CustomerAddress> addresses = null, List<CustomerPhone> phones = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer();
            c.Culture = culture;
            c.IsSubjectToBackupWithholding = isSubjectToBackupWithholding;
            c.IsOptedInToBankCommunication = isOptedInToBankCommunication;
            c.IsDocumentsAccepted = isDocumentsAccepted;
            c.Tag = tag;
            c.Gender = gender;
            c.FirstName = firstName;
            c.MiddleName = middleName;
            c.LastName = lastName;
            c.TaxId = taxId;
            c.BirthDate = birthDate;
            c.EmailAddress = emailAddress;
            c.DriversLicenseNumber = driversLicenseNumber;
            c.DriversLicenseState = driversLicenseState;
            c.DriversLicenseExpirationDate = driversLicenseExpirationDate;
            c.PassportNumber = passportNumber;
            c.PassportCountry = passportCountry;
            c.Addresses = addresses;
            c.Phones = phones;

            return c.Create(connection, userDefinedObjectForLogging);
        }


        /// <summary>
        /// Creates a customer with no identity verification performed.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual Customer Create(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomer = Requestor.Post<dynamic>("customer/create", connection, this, userDefinedObjectForLogging);
            var rv = Customer.Get((int)newCustomer.customerId, connection, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerResponse Initiate(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(customerId).Initiate(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Initiates the customer creation process.  Returns a list of questions the user must answer to verify their identity.  Should subsequently call Customer.Verify() with those answers.  The customer is not active until a subsequent call to Customer.Verify() has returned success.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerResponse Initiate(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerResponse>("customer/initiate", connection, this, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerResponse Verify(int? customerId, CustomerVerifyRequest request, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(customerId).Verify(request, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Finishes the customer creation process started by Customer.Initiate().  Requires the answers to the questions as well as the verificationId and question types supplied from the Customer.Initiate() call.  When this call has returned successfully, the customer is active.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public CustomerResponse Verify(CustomerVerifyRequest request, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerResponse>("customer/verify", connection, request, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerIdOnly Deactivate(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(customerId).Deactivate(connection, userDefinedObjectForLogging);
        }

        public virtual CustomerIdOnly Deactivate(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerIdOnly>("customer/deactivate", connection, this, userDefinedObjectForLogging);
            return rv;
        }


        public static List<Customer> Search(string tag = null, string taxId = null, string passportNumber = null, string driversLicenseNumber = null, DateTimeOffset? dateOfBirth = null, string emailAddress = null, string lastName = null, string firstName = null, int? pageNumber = 0, int? pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(0);
            c.Tag = tag;
            c.TaxId = taxId;
            c.PassportNumber = passportNumber;
            c.DriversLicenseNumber = driversLicenseNumber;
            c.BirthDate = dateOfBirth;
            c.EmailAddress = emailAddress;
            c.LastName = lastName;
            c.FirstName = firstName;

            return c.Search(pageNumber, pageSize, connection, userDefinedObjectForLogging);

        }

        /// <summary>
        /// Lists all customers matching given criteria.  Note this Addresses and Phones properties of returned Customer objects are not filled.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="taxId"></param>
        /// <param name="passportNumber"></param>
        /// <param name="driversLicenseNumber"></param>
        /// <param name="emailAddress"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual List<Customer> Search(int? pageNumber = 0, int? pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();

            var rv = Requestor.Post<List<Customer>>(String.Format("customer/search?pageNumber={0}&pageSize={1}", pageNumber, pageSize), connection, this, userDefinedObjectForLogging);
            return rv;
        }

        public static Customer Get(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(customerId).Get(connection, userDefinedObjectForLogging);
        }

        public virtual Customer Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Customer>(String.Format("customer/get/{0}", this.CustomerId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static Customer GetByTag(string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c= new Customer(0);
            c.Tag = tag;
            return c.GetByTag(connection, userDefinedObjectForLogging);
        }

        public virtual Customer GetByTag(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Customer>(String.Format("customer/getbytag/{0}", this.Tag), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerIdOnly Update(int? customerId,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(customerId);
            return c.Update(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Edits a customer.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerIdOnly Update(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerIdOnly>("customer/update", connection, this, userDefinedObjectForLogging);
            return rv;
        }

    }
}
