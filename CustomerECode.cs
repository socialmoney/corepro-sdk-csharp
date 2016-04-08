using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class CustomerECode : Models.ModelBase
    {
        public int? CustomerId { get; set; }
        public int? ProgramECodeId { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public int? FromAccountId { get; set; }
        public int? CashBoostAccountId { get; set; }
        public decimal? CashBoostAmount { get; set; }

        public int? CustomerECodeId { get; set; }
        public string OrderNumber { get; set; }
        public string MerchantCode { get; set; }
        public string EgcCode { get; set; }
        public string EgcAccessCode { get; set; }
        public string EgcUrl { get; set; }
        public string EgcChallenge { get; set; }
        public decimal? InitialBalance { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }

        public CustomerECode()
            : base()
        {
        }

        public CustomerECode(int? customerId, int? customerECodeId)
            : this()
        {
            this.CustomerId = customerId;
            this.CustomerECodeId = customerECodeId;
        }
        #region Synchronous
        public static CustomerECode Get(int? customerId, int? customerECodeId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new CustomerECode(customerId, customerECodeId).Get(connection, userDefinedObjectForLogging);
        }

        public virtual CustomerECode Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<CustomerECode>(String.Format("ecode/get/{0}/{1}", this.CustomerId, this.CustomerECodeId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static List<CustomerECode> List(int? customerId, int? customerECodeId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new CustomerECode(customerId, customerECodeId).List(connection, userDefinedObjectForLogging);
        }

        public virtual List<CustomerECode> List(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<CustomerECode>>(String.Format("ecode/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static CustomerECode Purchase(int? customerId, int? programECodeId = null, decimal? amount = null, int? fromAccountId = null, int? cashBoostAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ec = new CustomerECode();
            ec.CustomerId = customerId;
            ec.ProgramECodeId = programECodeId;
            ec.PurchaseAmount = amount;
            ec.FromAccountId = fromAccountId;
            ec.CashBoostAccountId = cashBoostAccountId;
            return ec.Purchase(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates a customer ecode using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerECode Purchase(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerECode>("ecode/purchase", connection, this, userDefinedObjectForLogging);
            //var newEcode = CustomerECode.Get(this.CustomerId, (int)rv.customerECodeId, connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static CustomerECode Reissue(int? customerId, int? programECodeId = null, int? customerECodeId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ec = new CustomerECode();
            ec.CustomerId = customerId;
            ec.ProgramECodeId = programECodeId;
            ec.CustomerECodeId = customerECodeId;
            return ec.Purchase(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Reissues a customer ecode using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual CustomerECode Reissue(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerECode>("ecode/reissue", connection, this, userDefinedObjectForLogging);
            //var newEcode = CustomerECode.Get(this.CustomerId, (int)rv.customerECodeId, connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }
        #endregion Synchronous








        #region Async
        public async static Task<CustomerECode> GetAsync(CancellationToken cancellationToken, int? customerId, int? customerECodeId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new CustomerECode(customerId, customerECodeId).GetAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<CustomerECode> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<CustomerECode>(cancellationToken, String.Format("ecode/get/{0}/{1}", this.CustomerId, this.CustomerECodeId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<List<CustomerECode>> ListAsync(CancellationToken cancellationToken, int? customerId, int? customerECodeId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new CustomerECode(customerId, customerECodeId).ListAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<CustomerECode>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<CustomerECode>>(cancellationToken, String.Format("ecode/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<CustomerECode> PurchaseAsync(CancellationToken cancellationToken, int? customerId, int? programECodeId = null, decimal? amount = null, int? fromAccountId = null, int? cashBoostAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ec = new CustomerECode();
            ec.CustomerId = customerId;
            ec.ProgramECodeId = programECodeId;
            ec.PurchaseAmount = amount;
            ec.FromAccountId = fromAccountId;
            ec.CashBoostAccountId = cashBoostAccountId;
            return await ec.PurchaseAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates a customer ecode using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerECode> PurchaseAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<CustomerECode>(cancellationToken, "ecode/purchase", connection, this, userDefinedObjectForLogging);
            //var newEcode = CustomerECode.Get(this.CustomerId, (int)rv.customerECodeId, connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<CustomerECode> ReissueAsync(CancellationToken cancellationToken, int? customerId, int? programECodeId = null, int? customerECodeId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ec = new CustomerECode();
            ec.CustomerId = customerId;
            ec.ProgramECodeId = programECodeId;
            ec.CustomerECodeId = customerECodeId;
            return await ec.PurchaseAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Reissues a customer ecode using properties from the current object.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async virtual Task<CustomerECode> ReissueAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<CustomerECode>(cancellationToken, "ecode/reissue", connection, this, userDefinedObjectForLogging);
            //var newEcode = CustomerECode.Get(this.CustomerId, (int)rv.customerECodeId, connection, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }
        #endregion Async
    }
}
