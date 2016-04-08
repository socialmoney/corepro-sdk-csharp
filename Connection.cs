using CorePro.SDK.Models;
using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    [DebuggerDisplay("DomainName={DomainName}, ApiKey={ApiKey}")]
    public class Connection
    {

        public static Connection CreateFromConfig(string apiKeyOverride = null, string apiSecretOverride = null)
        {
            var rv = new Connection();
            rv.ApiKey = apiKeyOverride ?? ConfigurationManager.AppSettings["CoreProApiKey"];
            rv.ApiSecret = apiSecretOverride ?? ConfigurationManager.AppSettings["CoreProApiSecret"];
            rv.DomainName = ConfigurationManager.AppSettings["CoreProDomainName"];
            if (string.IsNullOrEmpty(rv.DomainName))
                rv.DomainName = "api.corepro.io";

            return rv;
        }

        private static string normalizeDomainName(string value)
        {

            // given "https://domainname.tld" or "http://domainname.tld/page123" or "//domainname.tld/mypagehere?page=1" or "domainname.tld", return just "domainname.tld"
            
            if (String.IsNullOrEmpty(value))
                return value;
            else
                return value.Replace("https://", "").Replace("http://", "").Replace("//", "").Split('/')[0];
        }

        public Connection(string domainName, string apiKey = null, string apiSecret = null)
        {
            this.DomainName = domainName;
            this.ApiKey = apiKey;
            this.ApiSecret = apiSecret;
        }
        public Connection()
        {

        }

        private string _domainName;
        /// <summary>
        /// Gets or sets the DomainName where CorePro resides.  Should be just domainname. e.g. "api.corepro.io".  Defaults to "api.corepro.io" if not specified.
        /// </summary>
        public string DomainName { 
            get 
            {
                return _domainName;
            }
            set
            {
                _domainName = normalizeDomainName(value);
            }
        }

        private string _apiKey;
        /// <summary>
        /// Gets or sets the ApiKey to authenticate the request to CorePro.  Should be plaintext.
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
                _headerValue = null;
            }
        }

        private string _apiSecret;
        /// <summary>
        /// Gets or sets the ApiSecret to authenticate the request to CorePro.  Should be plaintext.
        /// </summary>
        public string ApiSecret
        {
            get
            {
                return _apiSecret;
            }
            set
            {
                _apiSecret = value;
                _headerValue = null;
            }
        }


        private string _headerValue;
        /// <summary>
        /// Gets the exact value to put into the Authorization header for a request to CorePro.  Essentially the following pseudocode:  return "Basic " + base64(ApiKey + ":" + ApiSecret)
        /// </summary>
        public string HeaderValue
        {
            get
            {
                if (_headerValue == null)
                {
                    try
                    {
                        if (String.IsNullOrWhiteSpace(ApiKey) || String.IsNullOrWhiteSpace(ApiSecret))
                            throw new InvalidOperationException("Both ApiKey and ApiSecret must be specified when making a CorePro request.");
                        var bytes = UTF8Encoding.UTF8.GetBytes(ApiKey + ":" + ApiSecret);
                        var b64 = Convert.ToBase64String(bytes);
                        _headerValue = "Basic " + b64;
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, null, null);
                        throw;
                    }
                }

                return _headerValue;
            }
        }
    }
}
