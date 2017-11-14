using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class CustomerDocument : ModelBase
    {
        public int? CustomerId { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }

        /// <summary>
        /// For uploading purposes only.  Should contain a Base64-encoded string of the raw file content.
        /// </summary>
        public string DocumentContent { get; set; }

        /// <summary>
        /// For downloading purposes only.  Pulling content from DownloadUrl will result in a FileContent object (i.e. equivalent to calling Download() on this object)
        /// </summary>
        public string DownloadUrl { get; set; }
        public string ReasonType { get; set; }
        public string Tag { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }

        public CustomerDocument() : base()
        {
        }

        public CustomerDocument(RequestMetaData metaData) : base(metaData)
        {

        }

        public CustomerDocument(int? customerId, int? documentId = null, RequestMetaData metaData = null)
            : this(metaData)
        {
            this.CustomerId = customerId;
            this.DocumentId = documentId;
        }

        #region Synchronous
        public static CustomerDocument Upload(int? customerId, string documentType, string documentName, byte[] rawContent, string reasonType, Connection connection = null, object userDefinedObjectForLogging = null, 
            string tag = null, string customField1 = null, string customField2 = null, string customField3 = null, string customField4 = null, string customField5 = null, 
            RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var cd = new CustomerDocument(customerId, null, metaData);
            cd.DocumentType = documentType;
            cd.DocumentName = documentName;
            cd.ReasonType = reasonType;
            cd.Tag = tag;
            cd.CustomField1 = customField1;
            cd.CustomField2 = customField2;
            cd.CustomField3 = customField3;
            cd.CustomField4 = customField4;
            cd.CustomField5 = customField5;
            cd.DocumentContent = Convert.ToBase64String(rawContent);
            return cd.Upload(connection, userDefinedObjectForLogging, metaData);
        }

        public virtual CustomerDocument Upload(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Post<CustomerDocument>("customerdocument/upload", connection, this, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public static CustomerDocument Get(int? customerId, int? documentId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = new CustomerDocument(customerId, documentId, metaData).Get(connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public virtual CustomerDocument Get(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<CustomerDocument>(String.Format("customerdocument/get/{0}/{1}", this.CustomerId, this.DocumentId), connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public static CustomerDocument GetByTag(int? customerId, string tag, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cd = new CustomerDocument(customerId, null, metaData);
            cd.Tag = tag;
            var rv = cd.GetByTag(connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public virtual CustomerDocument GetByTag(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<CustomerDocument>(String.Format("customerdocument/getByTag/{0}/?tag={1}", this.CustomerId, Uri.EscapeDataString(this.Tag + "")), connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public static List<CustomerDocument> List(int? customerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = new CustomerDocument(customerId, null, metaData).List(connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public virtual List<CustomerDocument> List(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<CustomerDocument>>(String.Format("customerdocument/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }


        public static FileContent Download(int? customerId, int documentId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = new CustomerDocument(customerId, documentId, metaData).Download(connection, userDefinedObjectForLogging, metaData);
            return rv;
        }

        public virtual FileContent Download(Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        { 
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<FileContent>(String.Format("customerdocument/download/{0}/{1}", this.CustomerId, this.DocumentId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }


        #endregion Synchronous

        #region Asynchronous
        public async static Task<CustomerDocument> UploadAsync(CancellationToken cancellationToken, int? customerId, string documentType, string documentName, byte[] documentContent, string reasonType, 
            Connection connection = null, object userDefinedObjectForLogging = null, string tag = null, string customField1 = null, string customField2 = null, string customField3 = null, string customField4 = null, string customField5 = null, 
            RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var cd = new CustomerDocument(customerId, null, metaData);
            cd.DocumentType = documentType;
            cd.DocumentName = documentName;
            cd.ReasonType = reasonType;
            cd.Tag = tag;
            cd.CustomField1 = customField1;
            cd.CustomField2 = customField2;
            cd.CustomField3 = customField3;
            cd.CustomField4 = customField4;
            cd.CustomField5 = customField5;
            cd.DocumentContent = Convert.ToBase64String(documentContent);
            return await cd.UploadAsync(cancellationToken, connection, userDefinedObjectForLogging, metaData);
        }


        public async virtual Task<CustomerDocument> UploadAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.PostAsync<CustomerDocument>(cancellationToken, "customerdocument/upload", connection, this, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public static async Task<CustomerDocument> GetAsync(CancellationToken cancellationToken, int? customerId, int? documentId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = (await new CustomerDocument(customerId, documentId, metaData).GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
            return rv;
        }

        public async virtual Task<CustomerDocument> GetAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = await this.GetAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public async virtual Task<Envelope<CustomerDocument>> GetAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<CustomerDocument>(cancellationToken, String.Format("customerdocument/get/{0}/{1}", this.CustomerId, this.DocumentId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }

        public static async Task<CustomerDocument> GetByTagAsync(CancellationToken cancellationToken, int? customerId, string tag, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var cd = new CustomerDocument(customerId, null, metaData);
            cd.Tag = tag;
            var rv = (await cd.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData)).Data;
            return rv;
        }

        public async virtual Task<CustomerDocument> GetByTagAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = await this.GetByTagAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public async virtual Task<Envelope<CustomerDocument>> GetByTagAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<CustomerDocument>(cancellationToken, String.Format("customerdocument/getByTag/{0}/?tag={1}", this.CustomerId, Uri.EscapeDataString(this.Tag + "")), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }


        public static async Task<List<CustomerDocument>> ListAsync(CancellationToken cancellationToken, int? customerId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = await new CustomerDocument(customerId, null, metaData).ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public async virtual Task<List<CustomerDocument>> ListAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = await this.ListAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public async virtual Task<Envelope<List<CustomerDocument>>> ListAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<CustomerDocument>>(cancellationToken, String.Format("customerdocument/list/{0}", this.CustomerId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }


        public async static Task<FileContent> DownloadAsync(CancellationToken cancellationToken, int? customerId, int documentId, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = await new CustomerDocument(customerId, documentId, metaData).DownloadAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public virtual async Task<FileContent> DownloadAsync(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            var rv = await this.DownloadAsyncEnvelope(cancellationToken, connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        public virtual async Task<Envelope<FileContent>> DownloadAsyncEnvelope(CancellationToken cancellationToken, Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<FileContent>(cancellationToken, String.Format("customerdocument/download/{0}/{1}", this.CustomerId, this.DocumentId), connection, userDefinedObjectForLogging, metaData ?? this.MetaData);
            return rv;
        }
        
        #endregion Asynchronous

    }
}
