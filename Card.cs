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
    public class Card : ModelBase
    {

        public Card() : base()
        {
            Accounts = new List<Account>();
        }

        public Card(RequestMetaData metaData = null) : base(metaData)
        {
            Accounts = new List<Account>();
        }

        public Card(int? customerId, int? cardId, RequestMetaData metaData = null)
            : this(metaData)
        {
            this.CustomerId = customerId;
            this.CardId = cardId;
        }


        public int? CustomerId { get; set; }
        public int? CardId { get; set; }
        public int? CardHolderCustomerId { get; set; }
        public string TypeCode { get; set; }
        public string VendorTypeCode { get; set; }
        public string Status { get; set; }
        public string Tag { get; set; }
        public string CardNumberMasked { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName{ get; set; }
        public int? ExpireMonth { get; set; }
        public int? ExpireYear { get; set; }
        public int? PrimaryAccountId { get; set; }
        public string LockTypeCode { get; set; }
        public string LockReasonTypeCode { get; set; }

        public DateTimeOffset? BirthDate { get; set; } // used by Verify() route only!

        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? RequestedDate { get; set; }
        public DateTimeOffset? VerifiedDate { get; set; }

        public DateTimeOffset? ReissuedDate { get; set; }
        public DateTimeOffset? DeniedDate { get; set; }

        public DateTimeOffset? ExpiredDate { get; set; }
        public DateTimeOffset? ArchivedDate { get; set; }
        public string NewPin { get; set; }

        public List<Account> Accounts { get; set; }

        public DateTimeOffset? LastModifiedDate { get; set; }

        public string ReissueReasonTypeCode { get; set; }

        #region Async
        public async static Task<List<Card>> ListAsync(CancellationToken cancellationToken, int? customerId, int? cardId = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Card(customerId, cardId, metaData).ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async static Task<Envelope<List<Card>>> ListAsyncEnvelope(CancellationToken cancellationToken, int? customerId, int? cardId = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Card(customerId, cardId, metaData).ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData));
        }

        public async virtual Task<List<Card>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async virtual Task<Envelope<List<Card>>> ListAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Card>>(cancellationToken, String.Format("card/list/{0}/{1}", this.CustomerId, this.CardId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public async static Task<Card> GetAsync(CancellationToken cancellationToken, int? customerId, int? cardId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Card(customerId, cardId, metaData).GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async static Task<Envelope<Card>> GetAsyncEnvelope(CancellationToken cancellationToken, int? customerId, int? cardId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await new Card(customerId, cardId, metaData).GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData));
        }

        public async virtual Task<Card> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await this.GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async virtual Task<Envelope<Card>> GetAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Card>(cancellationToken, String.Format("card/get/{0}/{1}", this.CustomerId, this.CardId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public async static Task<Card> GetByTagAsync(CancellationToken cancellationToken, int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, null, metaData);
            c.Tag = tag;
            return (await c.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async static Task<Envelope<Card>> GetByTagAsyncEnvelope(CancellationToken cancellationToken, int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, null, metaData);
            c.Tag = tag;
            return (await c.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData));
        }

        public async virtual Task<Card> GetByTagAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return (await this.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
        }

        public async virtual Task<Envelope<Card>> GetByTagAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Card>(cancellationToken, String.Format("card/getbytag/{0}/?tag={1}", this.CustomerId, Uri.EscapeDataString(this.Tag + "")), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public async static Task<Card> UpdateAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string nickName = null, string tag = null, int? primaryAccountId = null,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.NickName = nickName;
            c.Tag = tag;
            c.PrimaryAccountId = primaryAccountId;
            return await c.UpdateAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> UpdateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/update", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.Data.RequestId;
            return rv.Data;
        }

        public async static Task<Card> InitiateAsync(CancellationToken cancellationToken, int? customerId, int? cardHolderCustomerId, string firstName, string middleName, string lastName, string nickName, string tag, int? primaryAccountId, string typeCode, string vendorTypeCode, 
            Connection connection = null, object userDefinedObjectForLogging = null, string newPin = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, null, metaData);
            c.CardHolderCustomerId = cardHolderCustomerId;
            c.FirstName = firstName;
            c.MiddleName = middleName;
            c.LastName = lastName;
            c.NickName = nickName;
            c.Tag = tag;
            c.PrimaryAccountId = primaryAccountId;
            c.TypeCode = typeCode;
            c.VendorTypeCode = vendorTypeCode;
            c.NewPin = newPin;
            return await c.InitiateAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> InitiateAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/initiate", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
            {
                this.RequestId = rv.RequestId;
                if (rv.Data != null)
                {
                    this.CardId = (int)rv.Data.CardId;
                }
            }
            return rv.Data;
        }

        public async static Task<Card> VerifyAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string cardNumberMasked, DateTimeOffset? birthDate,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.CardNumberMasked = cardNumberMasked;
            c.BirthDate = birthDate;
            return await c.VerifyAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> VerifyAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/verify", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> ArchiveAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string cardNumberMasked, DateTimeOffset? birthDate,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.CardNumberMasked = cardNumberMasked;
            c.BirthDate = birthDate;
            return await c.ArchiveAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> ArchiveAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/archive", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> ResetPinAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string newPin,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.NewPin = newPin;
            return await c.ResetPinAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> ResetPinAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/resetpin", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> ReissueAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string newPin, string reissueReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.NewPin = newPin;
            c.ReissueReasonTypeCode = reissueReasonTypeCode;
            return await c.ReissueAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> ReissueAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/reissue", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> LockAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string lockReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.LockReasonTypeCode = lockReasonTypeCode;
            return await c.LockAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> LockAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/lock", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }


        public async static Task<Card> UnlockAsync(CancellationToken cancellationToken, int? customerId, int? cardId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            return await c.UnlockAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> UnlockAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/unlock", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> HotListAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string lockReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.LockReasonTypeCode = lockReasonTypeCode;
            return await c.HotListAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> HotListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/hotlist", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> LostOrStolenAsync(CancellationToken cancellationToken, int? customerId, int? cardId, string lockReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.LockReasonTypeCode = lockReasonTypeCode;
            return await c.LostOrStolenAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> LostOrStolenAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<Card>(cancellationToken, "card/lostorstolen", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv.Data;
        }

        public async static Task<Card> AddAccountAsync(CancellationToken cancellationToken, int? customerId, int? cardId, int? accountId, int? priority,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return await c.AddAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> AddAccountAsync(CancellationToken cancellationToken, int? accountId, int? priority, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(this.CustomerId, this.CardId, accountId, priority, metaData);
            return await c.AddAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async static Task<Card> RemoveAccountAsync(CancellationToken cancellationToken, int? customerId, int? cardId, int? accountId,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(customerId, cardId, accountId, null, metaData);
            return await c.RemoveAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> RemoveAccountAsync(CancellationToken cancellationToken, int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(this.CustomerId, this.CardId, accountId, null, metaData);
            return await c.RemoveAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async static Task<Card> ReprioritizeAccountAsync(CancellationToken cancellationToken, int? customerId, int? cardId, int? accountId, int? priority,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return await c.ReprioritizeAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        public async virtual Task<Card> ReprioritizeAccountAsync(CancellationToken cancellationToken, int? accountId, int? priority, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(this.CustomerId, this.CardId, accountId, priority, metaData);
            return await c.ReprioritizeAccountAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }

        #endregion Async


        #region Synchronous

        public static List<Card> List(int? customerId, int? cardId = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new Card(customerId, cardId, metaData).List(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual List<Card> List(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<Card>>(String.Format("card/list/{0}/{1}", this.CustomerId, this.CardId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static Card Get(int? customerId, int? cardId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            return new Card(customerId, cardId, metaData).Get(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Get(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Card>(String.Format("card/get/{0}/{1}", this.CustomerId, this.CardId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static Card GetByTag(int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, null, metaData);
            c.Tag = tag;
            return c.GetByTag(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card GetByTag(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Card>(String.Format("card/getbytag/{0}/?tag={1}", this.CustomerId, Uri.EscapeDataString(this.Tag + "")), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }


        public static Card Update(int? customerId, int? cardId, string nickName = null, string tag = null, int? primaryAccountId = null,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.NickName = nickName;
            c.Tag = tag;
            c.PrimaryAccountId = primaryAccountId;
            return c.Update(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Update(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/update", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card Initiate(int? customerId, int? cardHolderCustomerId, string firstName, string middleName, string lastName, string nickName, string tag, int? primaryAccountId, string typeCode, string vendorTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, string newPin = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, null, metaData);
            c.CardHolderCustomerId = cardHolderCustomerId;
            c.FirstName = firstName;
            c.MiddleName = middleName;
            c.LastName = lastName;
            c.NickName = nickName;
            c.Tag = tag;
            c.PrimaryAccountId = primaryAccountId;
            c.TypeCode = typeCode;
            c.VendorTypeCode = vendorTypeCode;
            c.NewPin = newPin;
            return c.Initiate(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Initiate(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/initiate", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
            {
                this.CardId = rv.CardId;
                this.RequestId = rv.RequestId;
            }
            return rv;
        }

        public static Card Verify(int? customerId, int? cardId, string cardNumberMasked, DateTimeOffset? birthDate,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.CardNumberMasked = cardNumberMasked;
            c.BirthDate = birthDate;
            return c.Verify(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Verify(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/verify", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card Archive(int? customerId, int? cardId,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            return c.Archive(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Archive(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/archive", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card ResetPin(int? customerId, int? cardId, string newPin,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            return c.ResetPin(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card ResetPin(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/resetpin", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card Reissue(int? customerId, int? cardId, string newPin, string reissueReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.NewPin = newPin;
            c.ReissueReasonTypeCode = reissueReasonTypeCode;
            return c.Reissue(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Reissue(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/reissue", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card Lock(int? customerId, int? cardId, string lockReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.LockReasonTypeCode = lockReasonTypeCode;
            return c.Lock(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Lock(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/lock", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card Unlock(int? customerId, int? cardId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            return c.Unlock(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card Unlock(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/unlock", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card HotList(int? customerId, int? cardId, string lockReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.LockReasonTypeCode = lockReasonTypeCode;
            return c.HotList(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card HotList(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/hotlist", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }

        public static Card LostOrStolen(int? customerId, int? cardId, string lockReasonTypeCode,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new Card(customerId, cardId, metaData);
            c.LockReasonTypeCode = lockReasonTypeCode;
            return c.LostOrStolen(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card LostOrStolen(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            // last 4 of CardNumber
            // birthdate of cardholder
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<Card>("card/lostorstolen", connection, this, userDefinedObjectForLogging, metaData);
            if (rv != null)
                this.RequestId = rv.RequestId;
            return rv;
        }


        public static Card AddAccount(int? customerId, int? cardId, int? accountId, int? priority,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return c.AddAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card AddAccount(int? accountId, int? priority, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(this.CustomerId, this.CardId, accountId, priority, metaData);
            return c.AddAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public static Card RemoveAccount(int? customerId, int? cardId, int? accountId, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(customerId, cardId, accountId, null, metaData);
            return c.RemoveAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card RemoveAccount(int? accountId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(this.CustomerId, this.CardId, accountId, null, metaData);
            return c.RemoveAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public static Card ReprioritizeAccount(int? customerId, int? cardId, int? accountId, int? priority,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(customerId, cardId, accountId, priority, metaData);
            return c.ReprioritizeAccount(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual Card ReprioritizeAccount(int? accountId, int? priority, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var c = new CardAccountLink(this.CustomerId, this.CardId, accountId, priority, metaData);
            return c.ReprioritizeAccount(connection, userDefinedObjectForLogging, metaData);
        }

        #endregion Synchronous

    }
}
