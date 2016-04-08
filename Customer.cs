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
        public string Suffix { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Culture { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string TaxId { get; set; }
        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseState { get; set; }
        public DateTimeOffset? DriversLicenseIssueDate { get; set; }
        public DateTimeOffset? DriversLicenseExpireDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportCountry { get; set; }
        public DateTimeOffset? PassportIssueDate { get; set; }
        public DateTimeOffset? PassportExpireDate { get; set; }
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
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }

        public DateTimeOffset? LastActivityDate { get; set;}

        public DateTimeOffset? DocumentsAcceptedDate { get; set; }
        public DateTimeOffset? ExpiredDate { get; set; }
        public DateTimeOffset? ManualReviewDate { get; set; }

        #region Synchronous
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
            string driversLicenseNumber = null, string driversLicenseState = null, DateTimeOffset? driversLicenseExpireDate = null, string passportNumber = null, string passportCountry = null,
            List<CustomerAddress> addresses = null, List<CustomerPhone> phones = null,
            Connection connection = null, object userDefinedObjectForLogging = null, string suffix = null, string customField1 = null, string customField2 = null, string customField3 = null, string customField4 = null, string customField5 = null)
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
            c.Suffix = suffix;
            c.TaxId = taxId;
            c.BirthDate = birthDate;
            c.EmailAddress = emailAddress;
            c.DriversLicenseNumber = driversLicenseNumber;
            c.DriversLicenseState = driversLicenseState;
            c.DriversLicenseExpireDate = driversLicenseExpireDate;
            c.PassportNumber = passportNumber;
            c.PassportCountry = passportCountry;
            c.Addresses = addresses;
            c.Phones = phones;
            c.CustomField1 = customField1;
            c.CustomField2 = customField2;
            c.CustomField3 = customField3;
            c.CustomField4 = customField4;
            c.CustomField5 = customField5;

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
            var newCustomer = Requestor.Post<Customer>("customer/create", connection, this, userDefinedObjectForLogging);
            var rv = Customer.Get((int)newCustomer.CustomerId, connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
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
            if (rv != null)
                this.RequestId = rv.RequestId;
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
            if (rv != null)
                request.RequestId = rv.RequestId;
            return rv;
        }

        [Obsolete("Please use Customer.Archive() instead.")]
        public static CustomerIdOnly Deactivate(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(customerId).Archive(connection, userDefinedObjectForLogging);
        }

        public static CustomerIdOnly Archive(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Customer(customerId).Archive(connection, userDefinedObjectForLogging);
        }

        public virtual CustomerIdOnly Archive(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerIdOnly>("customer/archive", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
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
            if (rv != null)
                this.RequestId = rv.RequestId;
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
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Customer GetByEmail(string emailAddress = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(0);
            c.EmailAddress = emailAddress;
            return c.GetByEmail(connection, userDefinedObjectForLogging);
        }

        public virtual Customer GetByEmail(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Customer>(String.Format("customer/getbyemail/{0}", this.EmailAddress), connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static CustomerIdOnly Update(int? customerId, string firstName = null, string middleName = null, string lastName = null, string suffix = null, DateTimeOffset? birthDate = null, string taxId = null,
            string driversLicenseNumber = null, string driversLicenseState = null, DateTimeOffset? driversLicenseIssueDate = null, DateTimeOffset? driversLicenseExpireDate = null,
            string passportNumber = null, string passportCountry = null, DateTimeOffset? passportIssueDate = null, DateTimeOffset? passportExpireDate = null,
            string emailAddress = null, string gender = null, string culture = null, bool? isSubjectToBackupWithholding = null, bool? isOptedInToBankCommunication = null,
            string tag = null, string customField1 = null, string customField2 = null, string customField3 = null, string customField4 = null, string customField5 = null,
            List<CustomerAddress> addresses = null, List<CustomerPhone> phones = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(customerId);
            c.FirstName = firstName;
            c.MiddleName = middleName;
            c.LastName = lastName;
            c.Suffix = suffix;
            c.BirthDate = birthDate;
            c.TaxId = taxId;
            c.DriversLicenseNumber = driversLicenseNumber;
            c.DriversLicenseState = driversLicenseState;
            c.DriversLicenseIssueDate = driversLicenseIssueDate;
            c.DriversLicenseExpireDate = driversLicenseExpireDate;
            c.PassportNumber = passportNumber;
            c.PassportCountry = passportCountry;
            c.PassportIssueDate = passportIssueDate;
            c.PassportExpireDate = passportExpireDate;
            c.EmailAddress = emailAddress;
            c.Gender = gender;
            c.Culture = culture;
            c.IsSubjectToBackupWithholding = isSubjectToBackupWithholding;
            c.IsOptedInToBankCommunication = isOptedInToBankCommunication;
            c.Tag = tag;
            c.CustomField1 = customField1;
            c.CustomField2 = customField2;
            c.CustomField3 = customField3;
            c.CustomField4 = customField4;
            c.CustomField5 = customField5;
            c.Addresses = addresses;
            c.Phones = phones;
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
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }
        #endregion Synchronous

        #region Async
        public async static Task<List<Customer>> ListAllAsync(CancellationToken cancellationToken, int? pageNumber, int? pageSize, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new Customer(0).ListAsync(cancellationToken, pageNumber, pageSize, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<Customer>> ListAsync(CancellationToken cancellationToken, int? pageNumber, int? pageSize, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Customer>>(cancellationToken, String.Format("customer/list?pageNumber={0}&pageSize={1}", pageNumber, pageSize), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<Customer> CreateAsync(CancellationToken cancellationToken, string culture, bool isSubjectToBackupWithholding, bool isOptedInToBankCommunication, bool isDocumentsAccepted,
            string tag = null, string gender = "U", string firstName = null, string middleName = null, string lastName = null, string taxId = null, DateTimeOffset? birthDate = null, string emailAddress = null,
            string driversLicenseNumber = null, string driversLicenseState = null, DateTimeOffset? driversLicenseExpireDate = null, string passportNumber = null, string passportCountry = null,
            List<CustomerAddress> addresses = null, List<CustomerPhone> phones = null,
            Connection connection = null, object userDefinedObjectForLogging = null, string suffix = null, string customField1 = null, string customField2 = null, string customField3 = null, string customField4 = null, string customField5 = null)
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
            c.Suffix = suffix;
            c.TaxId = taxId;
            c.BirthDate = birthDate;
            c.EmailAddress = emailAddress;
            c.DriversLicenseNumber = driversLicenseNumber;
            c.DriversLicenseState = driversLicenseState;
            c.DriversLicenseExpireDate = driversLicenseExpireDate;
            c.PassportNumber = passportNumber;
            c.PassportCountry = passportCountry;
            c.Addresses = addresses;
            c.Phones = phones;
            c.CustomField1 = customField1;
            c.CustomField2 = customField2;
            c.CustomField3 = customField3;
            c.CustomField4 = customField4;
            c.CustomField5 = customField5;

            return await c.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }


        /// <summary>
        /// Creates a customer with no identity verification performed.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<Customer> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var newCustomer = (await Requestor.PostAsync<Customer>(cancellationToken, "customer/create", connection, this, userDefinedObjectForLogging)).Data;
            var rv = Customer.Get(newCustomer.CustomerId, connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public async static Task<CustomerResponse> InitiateAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new Customer(customerId).InitiateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Initiates the customer creation process.  Returns a list of questions the user must answer to verify their identity.  Should subsequently call Customer.Verify() with those answers.  The customer is not active until a subsequent call to Customer.Verify() has returned success.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerResponse> InitiateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<CustomerResponse>(cancellationToken, "customer/initiate", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<CustomerResponse> VerifyAsync(CancellationToken cancellationToken, int? customerId, CustomerVerifyRequest request, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new Customer(customerId).VerifyAsync(cancellationToken, request, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Finishes the customer creation process started by Customer.Initiate().  Requires the answers to the questions as well as the verificationId and question types supplied from the Customer.Initiate() call.  When this call has returned successfully, the customer is active.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<CustomerResponse> VerifyAsync(CancellationToken cancellationToken, CustomerVerifyRequest request, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<CustomerResponse>(cancellationToken, "customer/verify", connection, request, userDefinedObjectForLogging);
            request.RequestId = rv.RequestId;
            return rv.Data;
        }

        [Obsolete("Please use Customer.ArchiveAsync() instead.")]
        public async static Task<CustomerIdOnly> DeactivateAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return (await new Customer(customerId).ArchiveAsync(cancellationToken, connection, userDefinedObjectForLogging));
        }

        public async static Task<CustomerIdOnly> ArchiveAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return (await new Customer(customerId).ArchiveAsync(cancellationToken, connection, userDefinedObjectForLogging));
        }

        public async virtual Task<CustomerIdOnly> ArchiveAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<CustomerIdOnly>(cancellationToken, "customer/archive", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }


        public async static Task<List<Customer>> SearchAsync(CancellationToken cancellationToken, string tag = null, string taxId = null, string passportNumber = null, string driversLicenseNumber = null, DateTimeOffset? dateOfBirth = null, string emailAddress = null, string lastName = null, string firstName = null, int? pageNumber = 0, int? pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
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

            return await c.SearchAsync(cancellationToken, pageNumber, pageSize, connection, userDefinedObjectForLogging);

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
        public async virtual Task<List<Customer>> SearchAsync(CancellationToken cancellationToken, int? pageNumber = 0, int? pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();

            var rv = await Requestor.PostAsync<List<Customer>>(cancellationToken, String.Format("customer/search?pageNumber={0}&pageSize={1}", pageNumber, pageSize), connection, this, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<Customer> GetAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new Customer(customerId).GetAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Customer> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Customer>(cancellationToken, String.Format("customer/get/{0}", this.CustomerId), connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Customer> GetByTagAsync(CancellationToken cancellationToken, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(0);
            c.Tag = tag;
            return await c.GetByTagAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Customer> GetByTagAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Customer>(cancellationToken, String.Format("customer/getbytag/{0}", this.Tag), connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Customer> GetByEmailAsync(CancellationToken cancellationToken, string emailAddress = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(0);
            c.EmailAddress = emailAddress;
            return await c.GetByEmailAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Customer> GetByEmailAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Customer>(cancellationToken, String.Format("customer/getbyemail/{0}", this.EmailAddress), connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Customer> UpdateAsync(CancellationToken cancellationToken, int? customerId, string firstName = null, string middleName = null, string lastName = null, string suffix = null, DateTimeOffset? birthDate = null, string taxId = null,
            string driversLicenseNumber = null, string driversLicenseState = null, DateTimeOffset? driversLicenseIssueDate = null, DateTimeOffset? driversLicenseExpireDate = null,
            string passportNumber = null, string passportCountry = null, DateTimeOffset? passportIssueDate = null, DateTimeOffset? passportExpireDate = null,
            string emailAddress = null, string gender = null, string culture = null, bool? isSubjectToBackupWithholding = null, bool? isOptedInToBankCommunication = null,
            string tag = null, string customField1 = null, string customField2 = null, string customField3 = null, string customField4 = null, string customField5 = null,
            List<CustomerAddress> addresses = null, List<CustomerPhone> phones = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var c = new Customer(customerId);
            c.FirstName = firstName;
            c.MiddleName = middleName;
            c.LastName = lastName;
            c.Suffix = suffix;
            c.BirthDate = birthDate;
            c.TaxId = taxId;
            c.DriversLicenseNumber = driversLicenseNumber;
            c.DriversLicenseState = driversLicenseState;
            c.DriversLicenseIssueDate = driversLicenseIssueDate;
            c.DriversLicenseExpireDate = driversLicenseExpireDate;
            c.PassportNumber = passportNumber;
            c.PassportCountry = passportCountry;
            c.PassportIssueDate = passportIssueDate;
            c.PassportExpireDate = passportExpireDate;
            c.EmailAddress = emailAddress;
            c.Gender = gender;
            c.Culture = culture;
            c.IsSubjectToBackupWithholding = isSubjectToBackupWithholding;
            c.IsOptedInToBankCommunication = isOptedInToBankCommunication;
            c.Tag = tag;
            c.CustomField1 = customField1;
            c.CustomField2 = customField2;
            c.CustomField3 = customField3;
            c.CustomField4 = customField4;
            c.CustomField5 = customField5;
            c.Addresses = addresses;
            c.Phones = phones;
            return await c.UpdateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Edits a customer.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<Customer> UpdateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Customer>(cancellationToken, "customer/update", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }
        #endregion Async
    }
}
