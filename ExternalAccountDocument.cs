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
    public class ExternalAccountDocument : ModelBase
    {
        public static void Upload(int? customerId, int? externalAccountId, string documentType, string documentName, byte[] documentContent, string reasonType, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = customerId, ExternalAccountId = externalAccountId, DocumentType = documentType, DocumentName = documentName, DocumentContent = Convert.ToBase64String(documentContent), ReasonType = reasonType };
            var rv = Requestor.Post<CustomerIdOnly>("externalaccountdocument/upload", connection, body, userDefinedObjectForLogging);
        }




        public async static Task UploadAsync(CancellationToken cancellationToken, int? customerId, int? externalAccountId, string documentType, string documentName, byte[] documentContent, string reasonType, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var body = new { CustomerId = customerId, ExternalAccountId = externalAccountId, DocumentType = documentType, DocumentName = documentName, DocumentContent = Convert.ToBase64String(documentContent), ReasonType = reasonType };
            var rv = await Requestor.PostAsync<CustomerIdOnly>(cancellationToken, "externalaccountdocument/upload", connection, body, userDefinedObjectForLogging);
        }
    }
}
