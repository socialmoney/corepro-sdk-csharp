﻿using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class AccountClose : ModelBase
    {
        public AccountClose() : base()
        {

        }
        public AccountClose(RequestMetaData metaData) : base(metaData)
        {

        }

        public AccountClose(int? customerId, int? accountId, RequestMetaData metaData = null)
            : this(metaData)
        {
            this.CustomerId = customerId;
            this.AccountId = accountId;
        }

        public int? CustomerId { get; set; }
        public int? AccountId { get; set; }
        public int? CloseToAccountId { get; set; }
        public long? TransactionId { get; set; }
        public string TransactionTag { get; set;}
        public string CloseReason { get; set; }
        public decimal? ClosingBalanceAmount { get; set; }
        public decimal? InterestPaidAmount { get; set; }
        public decimal? BackupWithholdingAmount { get; set; }
        public decimal? TotalClosingAmount { get; set; }
        public bool? IsClosedToExternalAccount { get; set; }

        #region Async
        public async static Task<AccountClose> CloseAsync(CancellationToken cancellationToken, int? customerId, int? accountId, int? closeToAccountId, string transactionTag, string closeReason, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ac = new AccountClose(customerId, accountId, metaData);
            ac.CloseToAccountId = closeToAccountId;
            ac.TransactionTag = transactionTag;
            ac.CloseReason = closeReason;

            return await ac.CloseAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<AccountClose> CloseAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<AccountClose>(cancellationToken, "account/close", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }
        #endregion Async

        public static AccountClose Close(int? customerId, int? accountId, int? closeToAccountId, string transactionTag, string closeReason, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var ac = new AccountClose(customerId, accountId, metaData);
            ac.CloseToAccountId = closeToAccountId;
            ac.TransactionTag = transactionTag;
            ac.CloseReason = closeReason;

            return ac.Close(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual AccountClose Close(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<AccountClose>("account/close", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

    }
}
