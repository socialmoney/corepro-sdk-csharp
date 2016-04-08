using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Iso8583
{
    public class Authorization : Models.ModelBase
    {
        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public int? TransferToAccountId { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string SettlementDate { get; set; }
        public string FunctionCode { get; set; }
        public string ProcessingCode { get; set; }
        public string MerchantCategoryCode { get; set; }
        public string CardTypeCode { get; set; }
        public string AuthorizationLifeCycleCode { get; set; }
        public string Tag { get; set; }
        public string CustomField1 { get; set; }

        // Remaining properties are output only
        public long? AuthorizationTransactionId { get; set; }
        public decimal? AuthorizationAmount { get; set; }
        public decimal? AccountBalance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? PendingBalance { get; set; }
        public decimal? TransferToAccountBalance { get; set; }
        public decimal? TransferToAvailableBalance { get; set; }
        public decimal? TransferToPendingBalance { get; set; }

        public Authorization()
            : base()
        {
        }

        public Authorization(int? customerId, int? accountId)
            : this()
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        #region Synchronous

        public static Authorization Request(int? customerId, int? accountId, decimal? amount, string description, string settlementDate, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string authorizationLifeCycleCode, string tag, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var auth = new Authorization();
            auth.CustomerId = customerId;
            auth.AccountId = accountId;
            auth.TransferToAccountId = transferToAccountId;
            auth.Amount = amount;
            auth.Description = description;
            auth.SettlementDate = settlementDate;
            auth.FunctionCode = functionCode;
            auth.ProcessingCode = processingCode;
            auth.MerchantCategoryCode = merchantCategoryCode;
            auth.CardTypeCode = cardTypeCode;
            auth.AuthorizationLifeCycleCode = authorizationLifeCycleCode;
            auth.Tag = tag;
            auth.CustomField1 = customField1;
            return auth.Request(connection, userDefinedObjectForLogging);
        }
        public virtual Authorization Request(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Authorization>("iso8583/authorization/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Authorization Advice(int? customerId, int? accountId, decimal? amount, string description, string settlementDate, string functionCode, string processingCode, string merchantCategoryCode, string authorizationLifeCycleCode, string cardTypeCode, string tag, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var auth = new Authorization();
            auth.CustomerId = customerId;
            auth.AccountId = accountId;
            auth.TransferToAccountId = transferToAccountId;
            auth.Amount = amount;
            auth.Description = description;
            auth.SettlementDate = settlementDate;
            auth.FunctionCode = functionCode;
            auth.ProcessingCode = processingCode;
            auth.MerchantCategoryCode = merchantCategoryCode;
            auth.CardTypeCode = cardTypeCode;
            auth.AuthorizationLifeCycleCode = authorizationLifeCycleCode;
            auth.Tag = tag;
            auth.CustomField1 = customField1;
            return auth.Advice(connection, userDefinedObjectForLogging);
        }
        public virtual Authorization Advice(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Authorization>("iso8583/authorization/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }
        #endregion Synchronous

        #region Asynchronous
        public async static Task<Authorization> RequestAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string settlementDate, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string authorizationLifeCycleCode, string tag, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var auth = new Authorization();
            auth.CustomerId = customerId;
            auth.AccountId = accountId;
            auth.TransferToAccountId = transferToAccountId;
            auth.Amount = amount;
            auth.Description = description;
            auth.SettlementDate = settlementDate;
            auth.FunctionCode = functionCode;
            auth.ProcessingCode = processingCode;
            auth.MerchantCategoryCode = merchantCategoryCode;
            auth.CardTypeCode = cardTypeCode;
            auth.AuthorizationLifeCycleCode = authorizationLifeCycleCode;
            auth.Tag = tag;
            auth.CustomField1 = customField1;
            return await auth.RequestAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Authorization> RequestAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Authorization>(cancellationToken, "iso8583/authorization/request", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Authorization> AdviceAsync(CancellationToken cancellationToken, int? customerId, int? accountId, decimal? amount, string description, string settlementDate, string functionCode, string processingCode, string merchantCategoryCode, string cardTypeCode, string authorizationLifeCycleCode, string tag, string customField1 = null, int? transferToAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var auth = new Authorization();
            auth.CustomerId = customerId;
            auth.AccountId = accountId;
            auth.TransferToAccountId = transferToAccountId;
            auth.Amount = amount;
            auth.Description = description;
            auth.SettlementDate = settlementDate;
            auth.FunctionCode = functionCode;
            auth.ProcessingCode = processingCode;
            auth.MerchantCategoryCode = merchantCategoryCode;
            auth.CardTypeCode = cardTypeCode;
            auth.AuthorizationLifeCycleCode = authorizationLifeCycleCode;
            auth.Tag = tag;
            auth.CustomField1 = customField1;
            return await auth.AdviceAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<Authorization> AdviceAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Authorization>(cancellationToken, "iso8583/authorization/advice", connection, this, userDefinedObjectForLogging);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        #endregion Asynchronous
    }

}