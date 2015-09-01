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

        public static event EventHandler<RequestErrorEventArgs> OnError;

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

        #region Sync methods
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
                    if (response != null)
                        response.Dispose();
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
                            Envelope<TResponse> envelope = null;
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.NotImplemented:
                                    return handleGracefully<TResponse>((int)response.StatusCode, 50501, "Not Implemented", requestBody, responseBody);
                                case HttpStatusCode.BadGateway:
                                    return handleGracefully<TResponse>((int)response.StatusCode, 50502, "Bad Gateway", requestBody, responseBody);
                                case HttpStatusCode.ServiceUnavailable:
                                    return handleGracefully<TResponse>((int)response.StatusCode, 50503, "Service Unavailable", requestBody, responseBody);
                                case HttpStatusCode.GatewayTimeout:
                                    return handleGracefully<TResponse>((int)response.StatusCode, 50504, "Gateway Timeout", requestBody, responseBody);
                                case HttpStatusCode.HttpVersionNotSupported:
                                    return handleGracefully<TResponse>((int)response.StatusCode, 50505, "Http Version Not Supported", requestBody, responseBody);
                                default:
                                    try
                                    {
                                        envelope = new JsonSerializer().Deserialize<Envelope<TResponse>>(responseBody);
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
                                            // prior to throwing the exception, allow caller to handle gracefully
                                            // use case: when throttling is exceeded, caller wants to bounce user out of site entirely as too many people are using it.
                                            // instead of adding exception handling everywhere a call is made to check for Status == 429, they may register an error handler
                                            // and perform action there instead.
                                            return handleGracefully(envelope);
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
                                        return handleGracefully<TResponse>((int)response.StatusCode, 50000 + (int)response.StatusCode, "HTTP error " + (int)response.StatusCode, requestBody, responseBody);
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

        private static Envelope<TResponse> handleGracefully<TResponse>(int statusCode, int errorCode, string errorMessage, string rawRequestBody, string rawResponseBody)
        {
            var envelope = new Envelope<TResponse>();
            envelope.Errors.Add(new Error { Code = errorCode, Message = errorMessage });
            envelope.Status = (int)statusCode;
            envelope.RawRequestBody = rawRequestBody;
            envelope.RawResponseBody = rawResponseBody;
            return handleGracefully(envelope);
        }

        private static Envelope<TResponse> handleGracefully<TResponse>(Envelope<TResponse> envelope)
        {
            var handled = false;
            if (OnError != null)
            {
                var reea = new RequestErrorEventArgs();
                reea.Envelope = envelope;
                OnError(new Object(), reea);
                handled = reea.IsHandled;
            }
            if (handled)
            {
                // error was handled gracefully according to the caller. just return the envelope.
                return envelope;
            }
            else
            {
                // error was not handled gracefully according to the caller. throw hard exception.
                throw new CoreProApiException(envelope.Errors, envelope.Status, envelope.RawResponseBody);
            }
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
        #endregion Sync methods



        #region Async methods


        private async static Task<Envelope<TResponse>> sendAsync<TResponse>(CancellationToken cancellationToken, HttpWebRequest req, Connection authToken, string format, string requestBody, object userDefinedObjectForLogging) where TResponse : class
        {
            // async/await paradigm does not allow lambda expressions. so we use the statsCallback() function instead.
            var rv = await Stats.RecordAsync<TResponse>(cancellationToken, async () => 
                {
                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)(await req.GetResponseAsync());
                        return await parseResponseAsync<TResponse>(cancellationToken, req, response, format, requestBody, userDefinedObjectForLogging);
                    }
                    catch (WebException we)
                    {
                        response = (HttpWebResponse)we.Response;
                        if (response != null)
                        {
                            // note: the follwing will always throw an ApiException.
                            //       we use it just to parse the errors nicely.
                            // also -- you cannot use await within an exception handler, so we use the synchronous version here.
                            parseResponse<object>(req, response, format, requestBody, userDefinedObjectForLogging);
                            return null; // to satisfy compiler
                        }
                        else
                        {
                            // probably invalid url. just rethrow so caller can deal with it.
                            try {
                                return handleGracefully<TResponse>((int)we.Status, 50000 + (int)we.Status, we.Message, requestBody, null);
                            }
                            catch (CoreProApiException)
                            {
                                // handleGracefully threw a CoreProApiException. this is a "friendly" version of the real exception.
                                // don't mask it; they had a chance to handle smoothly using Requestor.OnError but did not.
                                throw we;
                            }
                        }
                    }
                    finally
                    {
                        if (response != null)
                            response.Dispose();
                    }
                }, req);
            return rv;
        }


        private async static Task<Envelope<TResponse>> parseResponseAsync<TResponse>(CancellationToken cancellationToken, HttpWebRequest req, HttpWebResponse response, string format, string requestBody, object userDefinedObjectForLogging) where TResponse : class
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
                        responseBody = await reader.ReadToEndAsync();
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
        public async static Task<Envelope<TResponse>> GetAsync<TResponse>(CancellationToken cancellationToken, string relativeUrl, Connection connection, object userDefinedObject) where TResponse : class
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

                await Logger.WriteAsync(cancellationToken, req, null, userDefinedObject);
                return await sendAsync<TResponse>(cancellationToken, req, connection, "json", null, userDefinedObject);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, null, userDefinedObject);
                throw;
            }

        }

        public async static Task<Envelope<string>> DownloadAsync(CancellationToken cancellationToken, string relativeUrl, Connection connection, object userDefinedObjectForLogging = null)
        {
            try
            {
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

                await Logger.WriteAsync(cancellationToken, req, null, userDefinedObjectForLogging);

                return await sendAsync<string>(cancellationToken, req, connection, "csv", null, userDefinedObjectForLogging);
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
        public async static Task<Envelope<TResponse>> PostAsync<TResponse>(CancellationToken cancellationToken, string relativeUrl, Connection connection, object body, object userDefinedObjectForLogging) where TResponse : class
        {
            try
            {
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

                await Logger.WriteAsync(cancellationToken, request, bodyString, userDefinedObjectForLogging);

                return await sendAsync<TResponse>(cancellationToken, request, connection, "json", bodyString, userDefinedObjectForLogging);

            }
            catch (Exception ex)
            {
                Logger.Write(ex, null, userDefinedObjectForLogging);
                throw;
            }

        }
        #endregion Async methods

    }

}
