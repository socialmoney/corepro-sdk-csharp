using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class ExternalAccount : ModelBase
    {

        public ExternalAccount()
        {

        }

        public ExternalAccount(int? customerId, int? externalAccountId)
            : this()
        {
            this.CustomerId = customerId;
            this.ExternalAccountId = externalAccountId;
        }

        public int? CustomerId { get; set; }
        public int? ExternalAccountId { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? StatusDate { get; set; }
        public string RoutingNumber { get; set; }
        public string RoutingNumberMasked { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNumberMasked { get; set; }
        public string NOCCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public DateTimeOffset? LockedDate { get; set; }
        public string LockedReason { get; set; }

        public static List<ExternalAccount> List(int? customerId, int? externalAccountId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new ExternalAccount(customerId, externalAccountId).List(connection, userDefinedObjectForLogging);
        }

        public virtual List<ExternalAccount> List(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<ExternalAccount>>(String.Format("externalaccount/list/{0}/{1}", this.CustomerId, this.ExternalAccountId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static ExternalAccount Get(int? customerId, int? externalAccountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new ExternalAccount(customerId, externalAccountId).Get(connection, userDefinedObjectForLogging);
        }

        public virtual ExternalAccount Get(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<ExternalAccount>(String.Format("externalaccount/get/{0}/{1}", this.CustomerId, this.ExternalAccountId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static ExternalAccount GetByTag(int? customerId, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Tag = tag;
            return ea.GetByTag(connection, userDefinedObjectForLogging);
        }

        public virtual ExternalAccount GetByTag(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<ExternalAccount>(String.Format("externalaccount/getbytag/{0}/{1}", this.CustomerId, this.Tag), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static bool Verify(int? customerId, int? externalAccountId, decimal amount1, decimal amount2, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            return ea.Verify(amount1, amount2, connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses given parameters (or if null, corresponding properties on the current object) to verify the external account.  
        /// If successful, sets status to 'Verified' and returns true.  
        /// If failed too many times, will cause status to be set to 'Denied' (i.e. verify attempts has been exceeded -- see message text for # of attempts remaining).
        /// Also, status can be 'VerifyLocked' meaning an administrator locked the verification process, and this will fail.
        /// In failure results in this method throwing an exception with a code and descriptive reason as to why it failed.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="amount1"></param>
        /// <param name="amount2"></param>
        /// <param name="connection"></param>
        public virtual bool Verify(decimal amount1, decimal amount2, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, ExternalAccountId = this.ExternalAccountId, Amount1 = amount1, Amount2 = amount2 };
            Requestor.Post<object>("externalaccount/verify", connection, body, userDefinedObjectForLogging);
            return true;
        }

        public static bool Deactivate(int? customerId, int? externalAccountId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            return ea.Deactivate(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses given parameters (or if null, corresponding properties on the current object) to deactivate the external account.  Returns true on success, throws exception otherwise.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="externalAccountId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Deactivate(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = this.CustomerId, ExternalAccountId = this.ExternalAccountId };
            Requestor.Post<object>("externalaccount/deactivate", connection, body, userDefinedObjectForLogging);
            return true;
        }

        public static bool Update(int? customerId, int? externalAccountId, string nickName = null, string tag = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, externalAccountId);
            ea.NickName = nickName;
            ea.Tag = tag;
            return ea.Update(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Uses properties on current object to update the external account information.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool Update(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            Requestor.Post<object>("externalaccount/update", connection, this, userDefinedObjectForLogging);
            return true;
        }

        public static int Create(int? customerId, string name, string firstName, string lastName, string type,
            string routingNumber, string accountNumber, string nickName = null, string tag = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Name = name;
            ea.FirstName = firstName;
            ea.LastName = lastName;
            ea.NickName = nickName;
            ea.Type = type;
            ea.Tag = tag;
            ea.AccountNumber = accountNumber;
            ea.RoutingNumber = routingNumber;
            return ea.Create(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates an external account using properties on the current object.  Note it will be in a status of 'Unverified' until Verify is called unless your program is configured to allow external accounts to be auto-verified.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual int Create(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<dynamic>("externalaccount/create", connection, this, userDefinedObjectForLogging);
            this.ExternalAccountId = (int)rv.externalAccountId;
            return (int)this.ExternalAccountId;
        }

        public static int Initiate(int? customerId, string name, string firstName, string lastName, string type,
            string routingNumber, string accountNumber, string nickName = null, string tag = null,
            Connection connection = null, object userDefinedObjectForLogging = null)
        {
            var ea = new ExternalAccount(customerId, null);
            ea.Name = name;
            ea.FirstName = firstName;
            ea.LastName = lastName;
            ea.NickName = nickName;
            ea.Type = type;
            ea.Tag = tag;
            ea.AccountNumber = accountNumber;
            ea.RoutingNumber = routingNumber;
            return ea.Initiate(connection, userDefinedObjectForLogging);
        }

        /// <summary>
        /// Creates an external account using properties on the current object.  Note it will be in a status of 'Unverified' until Verify is called unless your program is configured to allow external accounts to be auto-verified.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual int Initiate(Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<dynamic>("externalaccount/initiate", connection, this, userDefinedObjectForLogging);
            this.ExternalAccountId = (int)rv.externalAccountId;
            return (int)this.ExternalAccountId;
        }

    }
}
