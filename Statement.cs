using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
