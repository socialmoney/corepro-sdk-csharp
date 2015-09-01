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
    public class Statement : ModelBase
    {
        public int? StatementId { get; set; }
        public int? CustomerId { get; set; }
        public string Type { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }

        #region Synchronous

        public static List<Statement> List(int customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<Statement>>(String.Format("statement/list/{0}", customerId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static Statement Get(int customerId, int? statementId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<Statement>(String.Format("statement/get/{0}/{1}", customerId, statementId), connection, userDefinedObjectForLogging);
            return rv;
        }

        public static FileContent Download(int customerId, int? statementId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<FileContent>(String.Format("statement/download/{0}/{1}", customerId, statementId), connection, userDefinedObjectForLogging);
            return rv;
        }
        #endregion Synchronous




        #region Async

        public async static Task<List<Statement>> ListAsync(CancellationToken cancellationToken, int customerId, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Statement>>(cancellationToken, String.Format("statement/list/{0}", customerId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<Statement> GetAsync(CancellationToken cancellationToken, int customerId, int? statementId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<Statement>(cancellationToken, String.Format("statement/get/{0}/{1}", customerId, statementId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

        public async static Task<FileContent> DownloadAsync(CancellationToken cancellationToken, int customerId, int? statementId = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<FileContent>(cancellationToken, String.Format("statement/download/{0}/{1}", customerId, statementId), connection, userDefinedObjectForLogging);
            return rv.Data;
        }
        #endregion Async
    }
}
