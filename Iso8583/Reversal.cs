using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Iso8583
{
    public class Reversal : Models.ModelBase
    {
        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public long? FinancialTransactionId { get; set; }
        public long? ReversalTransactionId { get; set; }
        public Reversal()
            : base()
        {
        }

        public Reversal(int? customerId, int? accountId)
            : this()
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        #region Synchronous

        public static Reversal Request(int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var reversal = new Reversal();
            reversal.CustomerId = customerId;
            reversal.AccountId = accountId;
            reversal.Amount = amount;
            reversal.Description = description;
            reversal.FinancialTransactionId = financialTransactionId;
            reversal.Tag = tag;
            return reversal.Request(connection, userDefinedObjectForLogging);
        }
        public virtual Reversal Request(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Reversal>("iso8583/reversal/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Reversal Advice(int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var reversal = new Reversal();
            reversal.CustomerId = customerId;
            reversal.AccountId = accountId;
            reversal.Amount = amount;
            reversal.Description = description;
            reversal.FinancialTransactionId = financialTransactionId;
            reversal.Tag = tag;
            return reversal.Advice(connection, userDefinedObjectForLogging);
        }
        public virtual Reversal Advice(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Reversal>("iso8583/reversal/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }
        #endregion Synchronous

        #region Asynchronous
        public async static Task<Reversal> RequestAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var reversal = new Reversal();
            reversal.CustomerId = customerId;
            reversal.AccountId = accountId;
            reversal.Amount = amount;
            reversal.Description = description;
            reversal.FinancialTransactionId = financialTransactionId;
            reversal.Tag = tag;
            return await reversal.RequestAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Reversal> RequestAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Reversal>(cancellationToken, "iso8583/reversal/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Reversal> AdviceAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string tag, long? financialTransactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var reversal = new Reversal();
            reversal.CustomerId = customerId;
            reversal.AccountId = accountId;
            reversal.Amount = amount;
            reversal.Description = description;
            reversal.FinancialTransactionId = financialTransactionId;
            reversal.Tag = tag;
            return await reversal.AdviceAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Reversal> AdviceAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Reversal>(cancellationToken, "iso8583/reversal/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        #endregion Asynchronous
    }

}