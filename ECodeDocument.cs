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
    public class ECodeDocument : ModelBase
    {
        public string ProductCode { get; set; }
        public int? DocumentId { get; set; }
        public string DocumentType { get; set; }
        public DateTimeOffset? EffectiveDate { get; set; }
        public DateTimeOffset? ExpireDate { get; set; }
        public string Culture { get; set; }
        public string Html { get; set; }

        public static List<ECodeDocument> List(int? programECodeId, string documentType = null, string cultureName = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<List<ECodeDocument>>(String.Format("ecodedocument/list/{0}/{1}/{2}", programECodeId, documentType, cultureName), connection, userDefinedObjectForLogging);
            return rv;
        }



        public async static Task<List<ECodeDocument>> ListAsync(CancellationToken cancellationToken, int? programECodeId, string documentType = null, string cultureName = null, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = await Requestor.GetAsync<List<ECodeDocument>>(cancellationToken, String.Format("ecodedocument/list/{0}/{1}/{2}", programECodeId, documentType, cultureName), connection, userDefinedObjectForLogging);
            return rv.Data;
        }
    }
}
