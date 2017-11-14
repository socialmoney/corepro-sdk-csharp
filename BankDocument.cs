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
    public class BankDocument : ModelBase
    {

        public BankDocument() : base()
        {

        }
        public BankDocument(RequestMetaData metaData) : base(metaData)
        {

        }

        public int? DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string Culture { get; set; }
        public string Html { get; set; }
        public string Title { get; set; }
        public string DownloadUrl { get; set; }
        public DateTimeOffset? EffectiveDate { get; set; }
        public DateTimeOffset? ExpireDate { get; set; }

        #region Async
        public async static Task<List<BankDocument>> ListAsync(CancellationToken cancellationToken, string cultureName, string documentType = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<BankDocument>>(cancellationToken, String.Format("bankdocument/list/{0}/{1}", cultureName, documentType), connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public async static Task<FileContent> Download(CancellationToken cancellationToken, string cultureName, int documentId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<FileContent>(cancellationToken, String.Format("bankdocument/download/{0}/{1}", cultureName, documentId), connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        #endregion Async

        public static List<BankDocument> List(string cultureName, string documentType = null, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<BankDocument>>(String.Format("bankdocument/list/{0}/{1}", cultureName, documentType), connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public static FileContent Download(string cultureName, int documentId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<FileContent>(String.Format("bankdocument/download/{0}/{1}", cultureName, documentId), connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

    }
}
