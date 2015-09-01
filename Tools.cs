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
    public class Tools
    {
        public static ExternalBank VerifyRoutingNumber(string routingNumber, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<ExternalBank>(String.Format("tools/verifyroutingnumber/{0}", routingNumber), connection, userDefinedObjectForLogging);
            return rv;
        }


        public async static Task<ExternalBank> VerifyRoutingNumberAsync(CancellationToken cancellationToken, string routingNumber, Connection connection = null, object userDefinedObjectForLogging = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            if (!String.IsNullOrEmpty(routingNumber)){
                routingNumber = Uri.EscapeUriString(routingNumber.Trim());
            }
            var rv = await Requestor.GetAsync<ExternalBank>(cancellationToken, String.Format("tools/verifyroutingnumber/{0}", routingNumber), connection, userDefinedObjectForLogging);
            return rv.Data;
        }


    }
}
