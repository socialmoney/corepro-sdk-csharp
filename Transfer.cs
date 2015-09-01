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
    public class Transfer : ModelBase
    {
        public Transfer()
        {

        }

        public Transfer(int? customerId, int? fromId, int? toId, decimal? amount, string tag = null, string nachaDescription = null)
            : this()
        {
            this.CustomerId = customerId;
            this.FromId = fromId;
            this.ToId = toId;
            this.Amount = amount;
            this.Tag = tag;
            this.NachaDescription = nachaDescription;
        }

        public int? CustomerId { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public decimal? Amount { get; set; }
        public string NachaDescription { get; set; }
        public string Tag { get; set; }
        public long? TransactionId { get; set; }

        #region Synchronous

        /// <summary>
        /// Creates a transfer in CorePro.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="amount"></param>
        /// <param name="tag"></param>
        /// <param name="nachaDescription"></param>
        /// <param name="connection"></param>
        /// <param name="userDefinedObjectForLogging"></param>
        /// <returns></returns>
        public static List<Transfer> Create(int? customerId, int? fromId, int? toId, decimal? amount, string tag = null, string nachaDescription = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var t = new Transfer(customerId, fromId, toId, amount, tag, nachaDescription);
            return t.Create(connection, userDefinedObjectForLogging);
        }

        public virtual List<Transfer> Create(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<List<Transfer>>("transfer/create", connection, this, userDefinedObjectForLogging);
            if (rv != null && rv.Count > 0)
            {
                this.RequestId = rv[0].RequestId;
            }
            return rv;
        }

        /// <summary>
        /// Voids a transaction in CorePro.  Transaction must have a Status of 'Initiated' to be successfully voided.  Only CustomerId and one of TransactionId or Tag are required.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="transactionId"></param>
        /// <param name="tag">Value provided for Tag at transfer create time.</param>
        /// <param name="connection"></param>
        /// <param name="userDefinedObjectForLogging"></param>
        /// <returns></returns>
        public static List<Transfer> Void(int? customerId, long? transactionId = null, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var t = new Transfer(customerId, null, null, null, tag, null);
            t.TransactionId = transactionId;
            return t.Void(connection, userDefinedObjectForLogging);
        }

        public virtual List<Transfer> Void(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<List<Transfer>>("transfer/void", connection, this, userDefinedObjectForLogging);
            if (rv != null && rv.Count > 0)
            {
                this.RequestId = rv[0].RequestId;
            }
            return rv;
        }
        #endregion Synchronous








        #region Async

        /// <summary>
        /// Creates a transfer in CorePro.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="amount"></param>
        /// <param name="tag"></param>
        /// <param name="nachaDescription"></param>
        /// <param name="connection"></param>
        /// <param name="userDefinedObjectForLogging"></param>
        /// <returns></returns>
        public async static Task<List<Transfer>> CreateAsync(CancellationToken cancellationToken, int? customerId, int? fromId, int? toId, decimal? amount, string tag = null, string nachaDescription = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var t = new Transfer(customerId, fromId, toId, amount, tag, nachaDescription);
            return await t.CreateAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<Transfer>> CreateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = (await Requestor.PostAsync<List<Transfer>>(cancellationToken, "transfer/create", connection, this, userDefinedObjectForLogging)).Data;
            if (rv != null && rv.Count > 0)
            {
                this.RequestId = rv[0].RequestId;
            }
            return rv;
        }

        /// <summary>
        /// Voids a transaction in CorePro.  Transaction must have a Status of 'Initiated' to be successfully voided.  Only CustomerId and one of TransactionId or Tag are required.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="transactionId"></param>
        /// <param name="tag">Value provided for Tag at transfer create time.</param>
        /// <param name="connection"></param>
        /// <param name="userDefinedObjectForLogging"></param>
        /// <returns></returns>
        public async static Task<List<Transfer>> VoidAsync(CancellationToken cancellationToken, int? customerId, long? transactionId = null, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var t = new Transfer(customerId, null, null, null, tag, null);
            t.TransactionId = transactionId;
            return await t.VoidAsync(cancellationToken, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<Transfer>> VoidAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = (await Requestor.PostAsync<List<Transfer>>(cancellationToken, "transfer/void", connection, this, userDefinedObjectForLogging)).Data;
            if (rv != null && rv.Count > 0)
            {
                this.RequestId = rv[0].RequestId;
            }
            return rv;
        }
        #endregion Async

    }
}
