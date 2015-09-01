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
    public class Document : ModelBase
    {
        public int? BankId { get; set; }
        public int? CustomerId { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string Culture { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public string DownloadUrl { get; set; }
        public DateTimeOffset? EffectiveDate { get; set; }
        public DateTimeOffset? ExpireDate { get; set; }

        public static List<Document> List(string cultureName, string documentType = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<Document>>(String.Format("document/list/{0}/{1}", cultureName, documentType), connection, userDefinedObjectForLogging);
            return rv;
        }


        public async static Task<List<Document>> ListAsync(CancellationToken cancellationToken, string cultureName, string documentType = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<Document>>(cancellationToken, String.Format("document/list/{0}/{1}", cultureName, documentType), connection, userDefinedObjectForLogging);
            return rv.Data;
        }

    }
}
