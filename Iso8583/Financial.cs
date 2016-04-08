using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Iso8583
{
    public class Financial : Models.ModelBase
    {
        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public int? TransferToAccountId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string FunctionCode { get; set; }
        public string ProcessingCode { get; set; }
        public string MerchantCategoryCode { get; set; }
        public string CardTypeCode { get; set; }

        public string Tag { get; set; }
        public string CustomField1 { get; set; }
        public long? AuthorizationTransactionId { get; set; }

        // Remaining properties are output only
        public long? FinancialTransactionId { get; set; }
        public decimal? AccountBalance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? PendingBalance { get; set; }
        public decimal? TransferToAccountBalance { get; set; }
        public decimal? TransferToAvailableBalance { get; set; }
        public decimal? TransferToPendingBalance { get; set; }

        public Financial()
            : base()
        {
        }

        public Financial(int? customerId, int? accountId)
            : this()
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        #region Synchronous

        public static Financial Request(int? customerId, int? accountId, decimal? amount, string description, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string tag, long? authorizationTransactionId, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var fin = new Financial();
            fin.CustomerId = customerId;
            fin.AccountId = accountId;
            fin.TransferToAccountId = transferToAccountId;
            fin.Amount = amount;
            fin.Description = description;
            fin.FunctionCode = functionCode;
            fin.ProcessingCode = processingCode;
            fin.MerchantCategoryCode = merchantCategoryCode;
            fin.CardTypeCode = cardTypeCode;
            fin.AuthorizationTransactionId = authorizationTransactionId;
            fin.Tag = tag;
            fin.CustomField1 = customField1;
            return fin.Request(connection, userDefinedObjectForLogging);
        }
        public virtual Financial Request(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Financial>("iso8583/financial/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Financial Advice(int? customerId, int? accountId, decimal? amount, string description, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string tag, long? authorizationTransactionId, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var fin = new Financial();
            fin.CustomerId = customerId;
            fin.AccountId = accountId;
            fin.TransferToAccountId = transferToAccountId;
            fin.Amount = amount;
            fin.Description = description;
            fin.FunctionCode = functionCode;
            fin.ProcessingCode = processingCode;
            fin.MerchantCategoryCode = merchantCategoryCode;
            fin.CardTypeCode = cardTypeCode;
            fin.AuthorizationTransactionId = authorizationTransactionId;
            fin.Tag = tag;
            fin.CustomField1 = customField1;
            return fin.Advice(connection, userDefinedObjectForLogging);
        }
        public virtual Financial Advice(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Financial>("iso8583/financial/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }
        #endregion Synchronous

        #region Asynchronous
        public async static Task<Financial> RequestAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string tag, long? authorizationTransactionId, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var fin = new Financial();
            fin.CustomerId = customerId;
            fin.AccountId = accountId;
            fin.TransferToAccountId = transferToAccountId;
            fin.Amount = amount;
            fin.Description = description;
            fin.FunctionCode = functionCode;
            fin.ProcessingCode = processingCode;
            fin.MerchantCategoryCode = merchantCategoryCode;
            fin.CardTypeCode = cardTypeCode;
            fin.AuthorizationTransactionId = authorizationTransactionId;
            fin.Tag = tag;
            fin.CustomField1 = customField1;
            return await fin.RequestAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Financial> RequestAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Financial>(cancellationToken, "iso8583/financial/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Financial> AdviceAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string tag, long? authorizationTransactionId, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var fin = new Financial();
            fin.CustomerId = customerId;
            fin.AccountId = accountId;
            fin.TransferToAccountId = transferToAccountId;
            fin.Amount = amount;
            fin.Description = description;
            fin.FunctionCode = functionCode;
            fin.ProcessingCode = processingCode;
            fin.MerchantCategoryCode = merchantCategoryCode;
            fin.CardTypeCode = cardTypeCode;
            fin.AuthorizationTransactionId = authorizationTransactionId;
            fin.Tag = tag;
            fin.CustomField1 = customField1;
            return await fin.AdviceAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Financial> AdviceAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Financial>(cancellationToken, "iso8583/financial/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        #endregion Asynchronous
    }

}