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
    public class Transaction : ModelBase
    {
        public Transaction()
        {

        }

        public Transaction(int? customerId)
        {
            this.CustomerId = customerId;
        }

        public int? TransactionCount { get; set; }
        public int? CustomerId { get; set; }
        public long? TransactionId { get; set; }
        public string Tag { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string Type { get; set; }
        public string TypeCode { get; set; }
        public string Status { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTimeOffset? SettledDate { get; set; }
        public DateTimeOffset? VoidedDate { get; set; }
        public string NachaDescription { get; set; }
        public string FriendlyDescription { get; set; }

        public DateTimeOffset? AvailableDate { get; set; }
        public string ReturnCode { get; set; }

        public bool? IsCredit { get; set; }

        public string FeeCode { get; set; }
        public string FeeDescription { get; set; }

        public string InstitutionName { get; set; }
        public string CustomField1 { get; set; }

        #region Synchronous
        /// <summary>
        /// Lists all transactions for given user for all accounts for all time.  Optionally can restrict to a given account or a given range of dates.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <param name="beginDate">Null or empty string means beginning of time.</param>
        /// <param name="endDate">Null or empty string means end of time</param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static List<Transaction> List(int? customerId, int? accountId, string status = null, DateTimeOffset? beginDate = null, DateTimeOffset? endDate = null, int pageNumber = 0, int pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return new Transaction(customerId).List(accountId, status, beginDate, endDate, pageNumber, pageSize, connection, userDefinedObjectForLogging);
        }

        public virtual List<Transaction> List(int? accountId, string status = null, DateTimeOffset? beginDate = null, DateTimeOffset? endDate = null, int pageNumber = 0, int pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var begin = beginDate == null ? "" : beginDate.Value.ToString("yyyy-MM-dd");
            var end = endDate == null ? "" : endDate.Value.ToString("yyyy-MM-dd");

            if (!String.IsNullOrWhiteSpace(end) && String.IsNullOrWhiteSpace(begin))
            {
                // if we want to specify an end date, we must also specify a begin date.
                // api assumes if only 1 date is specified, it is the begin date (not the end date).
                // by forcing a begin date here, we make sure the end date is respected as expected.
                begin = "1900-01-01";
            }

            var rv = Requestor.Get<List<Transaction>>(String.Format("transaction/list/{0}/{1}/{2}/{3}/{4}?pageNumber={5}&pageSize={6}", this.CustomerId, accountId, status, begin, end, pageNumber, pageSize), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static List<Transaction> GetByTag(int? customerId, string tag, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            return Requestor.Get<List<Transaction>>(String.Format("transaction/getbytag/{0}/{1}", customerId, tag), connection, userDefinedObjectForLogging);
        }

        public static List<Transaction> Get(int? customerId, long? transactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            return Requestor.Get<List<Transaction>>(String.Format("transaction/get/{0}/{1}", customerId, transactionId), connection, userDefinedObjectForLogging);
        }
        #endregion Synchronous

        #region Async
        /// <summary>
        /// Lists all transactions for given user for all accounts for all time.  Optionally can restrict to a given account or a given range of dates.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <param name="beginDate">Null or empty string means beginning of time.</param>
        /// <param name="endDate">Null or empty string means end of time</param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async static Task<List<Transaction>> ListAsync(CancellationToken cancellationToken, int? customerId, int? accountId, string status = null, DateTimeOffset? beginDate = null, DateTimeOffset? endDate = null, int pageNumber = 0, int pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            return await new Transaction(customerId).ListAsync(cancellationToken, accountId, status, beginDate, endDate, pageNumber, pageSize, connection, userDefinedObjectForLogging);
        }

        public async virtual Task<List<Transaction>> ListAsync(CancellationToken cancellationToken, int? accountId, string status = null, DateTimeOffset? beginDate = null, DateTimeOffset? endDate = null, int pageNumber = 0, int pageSize = 200, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var begin = beginDate == null ? "" : beginDate.Value.ToString("yyyy-MM-dd");
            var end = endDate == null ? "" : endDate.Value.ToString("yyyy-MM-dd");

            if (!String.IsNullOrEmpty(status)){
                status = Uri.EscapeUriString(status);
            }

            if (!String.IsNullOrWhiteSpace(end) && String.IsNullOrWhiteSpace(begin))
            {
                // if we want to specify an end date, we must also specify a begin date.
                // api assumes if only 1 date is specified, it is the begin date (not the end date).
                // by forcing a begin date here, we make sure the end date is respected as expected.
                begin = "1900-01-01";
            }

            var rv = await Requestor.GetAsync<List<Transaction>>(cancellationToken, String.Format("transaction/list/{0}/{1}/{2}/{3}/{4}?pageNumber={5}&pageSize={6}", this.CustomerId, accountId, status, begin, end, pageNumber, pageSize), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<List<Transaction>> GetByTagAsync(CancellationToken cancellationToken, int? customerId, string tag, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Transaction>>(cancellationToken, String.Format("transaction/getbytag/{0}/{1}", customerId, tag), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<List<Transaction>> GetAsync(CancellationToken cancellationToken, int? customerId, long? transactionId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Transaction>>(cancellationToken, String.Format("transaction/get/{0}/{1}", customerId, transactionId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }
        #endregion Async

    }
}
