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
        public static ExternalBank VerifyRoutingNumber(string routingNumber, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var rv = Requestor.Get<ExternalBank>(String.Format("tools/verifyroutingnumber/{0}", routingNumber), connection, userDefinedObjectForLogging, metaData);
            return rv;
        }


        public async static Task<ExternalBank> VerifyRoutingNumberAsync(CancellationToken cancellationToken, string routingNumber, 
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            if (!String.IsNullOrEmpty(routingNumber)){
                routingNumber = Uri.EscapeUriString(routingNumber.Trim());
            }
            var rv = await Requestor.GetAsync<ExternalBank>(cancellationToken, String.Format("tools/verifyroutingnumber/{0}", routingNumber), connection, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

        /// <summary>
        /// Test the public key encryption used for newPin (and possibly other properties, see API documentation for details).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="inputValue">Encrypted value that needs to be decrypted</param>
        /// <param name="algorithm">Algorithm used for decryption. Possible values: RSA-OAEP-SHA1-MGF1, RSA-OAEP-SHA256-MGF1, RSA-OAEP-SHA384-MGF1, RSA-OAEP-SHA512-MGF1</param>
        /// <param name="comment">For caller logging purposes only -- CorePro API simply passes the value back; does not affect behavior.</param>
        /// <param name="connection"></param>
        /// <param name="userDefinedObjectForLogging"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public async static Task<DecryptionTest> TestPublicKeyDecryption(CancellationToken cancellationToken, string inputValue, string algorithm, string comment = null,
            Connection connection = null, object userDefinedObjectForLogging = null, RequestMetaData metaData = null)
        {
            connection = connection ?? Connection.CreateFromConfig();
            var dt = new DecryptionTest();
            dt.InputValue = inputValue;
            dt.Algorithm = algorithm;
            dt.Comment = comment;
            var rv = await Requestor.PostAsync<DecryptionTest>(cancellationToken, "tools/testpublickeydecryption", connection, dt, userDefinedObjectForLogging, metaData);
            return rv.Data;
        }

    }
}
