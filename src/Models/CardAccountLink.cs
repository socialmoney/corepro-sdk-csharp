using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class CardAccountLink : ModelBase
    {
        public CardAccountLink() : base()
        {

        }

        public CardAccountLink(RequestMetaData metaData) : base(metaData)
        {

        }

        public CardAccountLink(int? customerId, int? cardId, int? accountId, int? priority, RequestMetaData metaData = null)
            : this(metaData)
        {
            this.CustomerId = customerId;
            this.CardId = cardId;
            this.AccountId = accountId;
            this.Priority = priority;
        }

        public int? CustomerId { get; set; }
        public int? CardId { get; set; }
        public int? AccountId { get; set; }
        public int? Priority { get; set; }

        #region Async
        public async static Task<Card> AddAccountAsync(CancellationToken cancellationToken, int? customerId, int? cardId, int? accountId, int? priority, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var crdal = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return await crdal.AddAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> AddAccountAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/addAccount", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> RemoveAccountAsync(CancellationToken cancellationToken, int? customerId, int? cardId, int? accountId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var crdal = new CardAccountLink(customerId, cardId, accountId, null, metaData);
            return await crdal.RemoveAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> RemoveAccountAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/removeAccount", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> ReprioritizeAccountAsync(CancellationToken cancellationToken, int? customerId, int? cardId, int? accountId, int? priority,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var crdal = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return await crdal.ReprioritizeAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> ReprioritizeAccountAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/reprioritizeAccount", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }
        #endregion Async

        public static Card AddAccount(int? customerId, int? cardId, int? accountId, int? priority, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var crdal = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return crdal.AddAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card AddAccount(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/addAccount", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card RemoveAccount(int? customerId, int? cardId, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var crdal = new CardAccountLink(customerId, cardId, accountId, null, metaData);
            return crdal.RemoveAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card RemoveAccount(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/removeAccount", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card ReprioritizeAccount(int? customerId, int? cardId, int? accountId, int? priority, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var crdal = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return crdal.ReprioritizeAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card ReprioritizeAccount(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/reprioritizeAccount", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

    }
}
