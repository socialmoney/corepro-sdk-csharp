using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Iso8583
{
    public class Chargeback : Models.ModelBase
    {
        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public long? FinancialTransactionId { get; set; }
        public long? ChargebackTransactionId { get; set; }
        public Chargeback()
            : base()
        {
        }

        public Chargeback(int? customerId, int? accountId)
            : this()
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        #region Synchronous

        public static Chargeback Request(int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var chargeback = new Chargeback();
            chargeback.CustomerId = customerId;
            chargeback.AccountId = accountId;
            chargeback.Amount = amount;
            chargeback.Description = description;
            chargeback.FinancialTransactionId = financialTransactionId;
            chargeback.Tag = tag;
            return chargeback.Request(connection, userDefinedObjectForLogging);
        }
        public virtual Chargeback Request(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Chargeback>("iso8583/chargeback/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Chargeback Advice(int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var chargeback = new Chargeback();
            chargeback.CustomerId = customerId;
            chargeback.AccountId = accountId;
            chargeback.Amount = amount;
            chargeback.Description = description;
            chargeback.FinancialTransactionId = financialTransactionId;
            chargeback.Tag = tag;
            return chargeback.Advice(connection, userDefinedObjectForLogging);
        }
        public virtual Chargeback Advice(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Chargeback>("iso8583/chargeback/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }
        #endregion Synchronous

        #region Asynchronous
        public async static Task<Chargeback> RequestAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var chargeback = new Chargeback();
            chargeback.CustomerId = customerId;
            chargeback.AccountId = accountId;
            chargeback.Amount = amount;
            chargeback.Description = description;
            chargeback.FinancialTransactionId = financialTransactionId;
            chargeback.Tag = tag;
            return await chargeback.RequestAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Chargeback> RequestAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Chargeback>(cancellationToken, "iso8583/chargeback/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Chargeback> AdviceAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var chargeback = new Chargeback();
            chargeback.CustomerId = customerId;
            chargeback.AccountId = accountId;
            chargeback.Amount = amount;
            chargeback.Description = description;
            chargeback.FinancialTransactionId = financialTransactionId;
            chargeback.Tag = tag;
            return await chargeback.AdviceAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Chargeback> AdviceAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Chargeback>(cancellationToken, "iso8583/chargeback/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        #endregion Asynchronous
    }

}