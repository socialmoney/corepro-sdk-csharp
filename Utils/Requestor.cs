using CorePro.SDK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    public class Requestor
    {

        public static bool CertificateCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            // TODO: validate ssl cert here. i.e. check expire date, domain origination, chain links, etc.
            return true;
        }

        public Requestor()
        {
        }


        public static string SdkUserAgent {
            get {
                return "CorePro .NET SDK v " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        public static long SecondsSinceUnixEpoch
        {
            get
            {
                return TicksSinceUnixEpoch / 1000000L;
            }
        }

        public static long TicksSinceUnixEpoch
        {
            get
            {
                var epochTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
                return DateTime.UtcNow.Ticks - epochTicks;
            }
        }

        private static TResponse send<TResponse>(HttpWebRequest req, Connection authToken, string format, string requestBody, object userDefinedObjectForLogging) where TResponse : class
        {

            HttpWebResponse response = null;
            var rv = Stats.Record<TResponse>(() =>
            {
                try
                {
                    response = (HttpWebResponse)req.GetResponse();
                    return parseResponse<TResponse>(req, response, format, requestBody, userDefinedObjectForLogging);
                }
                catch (WebException we)
                {
                    response = (HttpWebResponse)we.Response;
                    if (response != null)
                    {
                        // note: the follwing will always throw an ApiException.
                        //       we use it just to parse the errors nicely.
                        parseResponse<object>(req, response, format, requestBody, userDefinedObjectForLogging);
                    }
                    else
                    {
                        // probably invalid url. just rethrow so caller can deal with it.
                        throw;
                    }
                }
                finally
                {
#if NET45
                    if (response != null)
                        response.Dispose();
#endif
                }
                return null;
            }, req);

            return rv;
        }



        private static Envelope<TResponse> parseResponse<TResponse>(HttpWebRequest req, HttpWebResponse response, string format, string requestBody, object userDefinedObjectForLogging) where TResponse : class
        {

            var code = response.StatusCode;
            string responseBody = null;
            try
            {

                var stream = response.GetResponseStream();

                if (stream != null)
                {
                    using (var reader = new StreamReader(stream, new UTF8Encoding(false, true)))
                    {
                        responseBody = reader.ReadToEnd();
                        if (format == "json")
                        {
                            var errors = new List<Error>();
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.NotImplemented:
                                    errors.Add(new Error { Code = 50501, Message = "Not Implemented" });
                                    throw new CoreProApiException(errors, (int)response.StatusCode, responseBody);
                                case HttpStatusCode.BadGateway:
                                    errors.Add(new Error { Code = 50502, Message = "Bad Gateway" });
                                    throw new CoreProApiException(errors, (int)response.StatusCode, responseBody);
                                case HttpStatusCode.ServiceUnavailable:
                                    errors.Add(new Error { Code = 50503, Message = "Service Unavailable" });
                                    throw new CoreProApiException(errors, (int)response.StatusCode, responseBody);
                                case HttpStatusCode.GatewayTimeout:
                                    errors.Add(new Error { Code = 50504, Message = "Gateway Timeout" });
                                    throw new CoreProApiException(errors, (int)response.StatusCode, responseBody);
                                case HttpStatusCode.HttpVersionNotSupported:
                                    errors.Add(new Error { Code = 50505, Message = "Http Version Not Supported" });
                                    throw new CoreProApiException(errors, (int)response.StatusCode, responseBody);
                                default:
                                    try
                                    {
                                        var envelope = new JsonSerializer().Deserialize<Envelope<TResponse>>(responseBody);
                                        envelope.RawRequestBody = requestBody;
                                        envelope.RawResponseBody = responseBody;

                                        try
                                        {
                                            if (envelope.Data != null)
                                            {
                                                var mb = envelope.Data as ModelBase;
                                                if (mb != null)
                                                {
                                                    mb.RequestId = envelope.RequestId;
                                                }
                                                else
                                                {
                                                    var listType = envelope.Data as IEnumerable<object>;
                                                    if (listType != null)
                                                    {
                                                        foreach (var item in listType)
                                                        {
                                                            var pi = item.GetType().GetProperty("RequestId");
                                                            if (pi != null)
                                                            {
                                                                pi.SetValue(item, envelope.RequestId, null);
                                                            }
                                                        }
                                                    }
                                                }

                                            }


                                        }
                                        catch (Exception exEat)
                                        {
                                            // do not do anything with this?
                                            Debug.WriteLine(exEat.ToString());
                                        }
                                        if ((envelope.Errors != null && envelope.Errors.Count > 0) || (envelope.Status != 200 && envelope.Status != 201))
                                        {
                                            throw new CoreProApiException(envelope.Errors, envelope.Status, envelope.RawResponseBody);
                                        }
                                        else
                                        {
                                            return envelope;
                                        }

                                    }
                                    catch (JsonReaderException)
                                    {
                                        // output is not valid JSON (parse failed).
                                        // just throw out the actual status code from the response
                                        errors.Add(new Error { Code = 50000 + (int)response.StatusCode, Message = "HTTP error " + (int)response.StatusCode });
                                        throw new CoreProApiException(errors, (int)response.StatusCode, responseBody);
                                    }
                            }
                        }
                        else
                            throw new NotImplementedException("Only json data is currently supported");

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, null, userDefinedObjectForLogging);
                throw;
            } 
            finally 
            {
                Logger.Write(response, responseBody, userDefinedObjectForLogging);
            }
            return null;
        }

        /// <summary>
        /// If optional parameters are not specified, they default to values stored in property of same name.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativeUrl"></param>
        /// <param name="connection">Info to put into the Authorization header.</param>
        /// <returns></returns>
        public static TResponse Get<TResponse>(string relativeUrl, Connection connection, object userDefinedObject) where TResponse : class
        {
            try
            {
                // required for ssl
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CertificateCallback);

                var absoluteUrl = "https://" + connection.DomainName + "/" + relativeUrl;
                var req = (HttpWebRequest)WebRequest.Create(absoluteUrl);
                req.Method = "GET";
                req.UserAgent = SdkUserAgent;
                req.Accept = "application/json; charset=utf-8";
                req.ContentType = "application/json; charset=utf-8";
                if (connection != null)
                    req.Headers.Add("Authorization", connection.HeaderValue);

                Logger.Write(req, null, userDefinedObject);
                return send<TResponse>(req, connection, "json", null, userDefinedObject);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, null, userDefinedObject);
                throw;
            }

        }

        public static object Download(string relativeUrl, Connection connection, object userDefinedObjectForLogging = null)
        {
            try { 
                // required for ssl
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CertificateCallback);

                var absoluteUrl = "https://" + connection.DomainName + "/" + relativeUrl;
                var req = (HttpWebRequest)WebRequest.Create(absoluteUrl);
                req.Method = "GET";
                req.UserAgent = SdkUserAgent;

                req.Accept = "text/csv; charset=utf-8";
                req.ContentType = "text/csv; charset=utf-8";

                if (connection != null)
                    req.Headers.Add("Authorization", connection.HeaderValue);

                Logger.Write(req, null, userDefinedObjectForLogging);

                return send<string>(req, connection, "csv", null, userDefinedObjectForLogging);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, null, userDefinedObjectForLogging);
                throw;
            }

        }

        /// <summary>
        /// If optional parameters are not specified, they default to values stored in property of same name.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="relativeUrl"></param>
        /// <param name="connection">Info to put into the Authorization header.</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static TResponse Post<TResponse>(string relativeUrl, Connection connection, object body, object userDefinedObjectForLogging) where TResponse : class
        {
            try { 
                var absoluteUrl = "https://" + connection.DomainName + "/" + relativeUrl;
                var request = (HttpWebRequest)WebRequest.Create(absoluteUrl);
                // required for ssl
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CertificateCallback);

                request.Method = "POST";
                request.UserAgent = SdkUserAgent;
                request.Accept = "application/json; charset=utf-8";
                request.ContentType = "application/json; charset=utf-8";

                if (connection != null)
                    request.Headers.Add("Authorization", connection.HeaderValue);

                var bodyString = new JsonSerializer().Serialize(body);
                var bytes = Encoding.UTF8.GetBytes(bodyString);
                request.ContentLength = bytes.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(bytes, 0, bytes.Length);
                }

                request.KeepAlive = true;

                Logger.Write(request, bodyString, userDefinedObjectForLogging);

                return send<TResponse>(request, connection, "json", bodyString, userDefinedObjectForLogging);

            }
            catch (Exception ex)
            {
                Logger.Write(ex, null, userDefinedObjectForLogging);
                throw;
            }

        }

    }

}
