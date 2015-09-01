using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    public class Logger
    {
        private static string __logFilePath = ConfigurationManager.AppSettings["CoreProLogFilePath"];
        /// <summary>
        /// Gets or sets the path to which all http requests to CorePro are logged.  Defaults to value in app setting CoreProLogFilePath.  If LogFilePath is null or empty string when Write() is called, no log is written.
        /// NOTE: Personally Identifiable Information (PII) may be written to your storage device if LogFilePath is specified either via code or the CoreProLogFilePath app setting.  Also, this file will grow infinitely large over time.  It is recommended this
        ///       logging capability not be used in production environments except while troubleshooting issues.  i.e. use with caution!
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                return __logFilePath;
            }
            set
            {
                __logFilePath = value;
            }
        }

        public static bool IsLogEnabled()
        {
            return !String.IsNullOrEmpty(LogFilePath) || __registeredLogWriter != null;
        }

        private static ILogWriter __registeredLogWriter;
        public static void RegisterLogWriter(ILogWriter logWriter)
        {
            __registeredLogWriter = logWriter;
        }

        #region Synchronous
        public static void Write(HttpWebRequest req, string body, object userDefinedObject)
        {
            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    __registeredLogWriter.Write(now, threadId, req, body, userDefinedObject);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex);
                }
            }

            if (IsLogEnabled())
            {
                var sb = new StringBuilder();
                sb.AppendLine("Request=");
                sb.Append(req.Method).Append(" ").Append(req.RequestUri.ToString()).Append(" HTTP/").AppendLine(req.ProtocolVersion.ToString());
                foreach (var key in req.Headers.AllKeys)
                {
                    sb.Append(key).Append(": ").AppendLine(req.Headers[key]);
                }

                if (!String.IsNullOrEmpty(body))
                {
                    sb.AppendLine();
                    sb.AppendLine(body);
                }

                writeToLogFile(now, threadId, sb.ToString(), userDefinedObject);
            }

        }

        public static void Write(HttpWebResponse resp, string body, object userDefinedObject)
        {
            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    __registeredLogWriter.Write(now, threadId, resp, body, userDefinedObject);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex);
                }
            }

            if (IsLogEnabled())
            {
                var sb = new StringBuilder();
                sb.AppendLine("Request=");
                sb.Append(resp.Method).Append(" ").Append(resp.ResponseUri.ToString()).Append(" HTTP/").AppendLine(resp.ProtocolVersion.ToString());
                foreach (var key in resp.Headers.AllKeys)
                {
                    sb.Append(key).Append(": ").AppendLine(resp.Headers[key]);
                }

                if (!String.IsNullOrEmpty(body))
                {
                    sb.AppendLine();
                    sb.AppendLine(body);
                }

                writeToLogFile(now, threadId, sb.ToString(), userDefinedObject);
            }

        }

        public static void Write(string body, object userDefinedObject)
        {
            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    __registeredLogWriter.Write(now, threadId, body, userDefinedObject);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex);
                }
            }

            writeToLogFile(now, threadId, body, userDefinedObject);

        }

        public static void Write(Exception ex, string additionalInfo, object userDefinedObject)
        {

            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    __registeredLogWriter.Write(now, threadId, ex, additionalInfo, userDefinedObject);
                }
                catch (Exception ex2)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex2);
                }
            }

            writeToLogFile(now, threadId, ex.Message + ". " + additionalInfo + ". " + ex.StackTrace, userDefinedObject);
        }

        private static void writeToLogFile(DateTimeOffset timestamp, int managedThreadId, string body, object userDefinedObject)
        {

            var logFile = LogFilePath;
            if (!String.IsNullOrEmpty(logFile))
            {
                try
                {
                    var dirPath = Path.GetDirectoryName(logFile);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    using (var sw = File.AppendText(logFile))
                    {
                        sw.WriteLine("#|" + timestamp.ToString("yyyy-MM-dd hh:mm:ss.fffffff tt") + "|" + managedThreadId + "|" + body + "|" + userDefinedObject);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Could not write to CorePro SDK log file '" + logFile + "': " + ex.Message + ".  See inner exception for more detail.", ex);
                }
            }

        }
        #endregion Synchronous

        #region Async

        public async static Task WriteAsync(CancellationToken cancellationToken, HttpWebRequest req, string body, object userDefinedObject)
        {
            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    await __registeredLogWriter.WriteAsync(cancellationToken, now, threadId, req, body, userDefinedObject);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex);
                }
            }

            if (IsLogEnabled())
            {
                var sb = new StringBuilder();
                sb.AppendLine("Request=");
                sb.Append(req.Method).Append(" ").Append(req.RequestUri.ToString()).Append(" HTTP/").AppendLine(req.ProtocolVersion.ToString());
                foreach (var key in req.Headers.AllKeys)
                {
                    sb.Append(key).Append(": ").AppendLine(req.Headers[key]);
                }

                if (!String.IsNullOrEmpty(body))
                {
                    sb.AppendLine();
                    sb.AppendLine(body);
                }

                writeToLogFile(now, threadId, sb.ToString(), userDefinedObject);
            }

        }

        public async static Task WriteAsync(CancellationToken cancellationToken, HttpWebResponse resp, string body, object userDefinedObject)
        {
            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    await __registeredLogWriter.WriteAsync(cancellationToken, now, threadId, resp, body, userDefinedObject);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex);
                }
            }

            if (IsLogEnabled())
            {
                var sb = new StringBuilder();
                sb.AppendLine("Request=");
                sb.Append(resp.Method).Append(" ").Append(resp.ResponseUri.ToString()).Append(" HTTP/").AppendLine(resp.ProtocolVersion.ToString());
                foreach (var key in resp.Headers.AllKeys)
                {
                    sb.Append(key).Append(": ").AppendLine(resp.Headers[key]);
                }

                if (!String.IsNullOrEmpty(body))
                {
                    sb.AppendLine();
                    sb.AppendLine(body);
                }

                writeToLogFile(now, threadId, sb.ToString(), userDefinedObject);
            }

        }

        public async static Task Write(CancellationToken cancellationToken, string body, object userDefinedObject)
        {
            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    await __registeredLogWriter.WriteAsync(cancellationToken, now, threadId, body, userDefinedObject);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex);
                }
            }

            writeToLogFile(now, threadId, body, userDefinedObject);

        }

        public async static Task WriteAsync(CancellationToken cancellationToken, Exception ex, string additionalInfo, object userDefinedObject)
        {

            var now = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (__registeredLogWriter != null)
            {
                try
                {
                    await __registeredLogWriter.WriteAsync(cancellationToken, now, threadId, ex, additionalInfo, userDefinedObject);
                }
                catch (Exception ex2)
                {
                    throw new InvalidOperationException("Calling Write() method on registered ILogWriter instance failed.  See inner exception for detail.", ex2);
                }
            }

            writeToLogFile(now, threadId, ex.Message + ". " + additionalInfo + ". " + ex.StackTrace, userDefinedObject);
        }

        private async static void writeToLogFileAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, string body, object userDefinedObject)
        {

            var logFile = LogFilePath;
            if (!String.IsNullOrEmpty(logFile))
            {
                try
                {
                    var dirPath = Path.GetDirectoryName(logFile);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    using (var sw = File.AppendText(logFile))
                    {
                        await sw.WriteLineAsync("#|" + timestamp.ToString("yyyy-MM-dd hh:mm:ss.fffffff tt") + "|" + managedThreadId + "|" + body + "|" + userDefinedObject);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Could not write to CorePro SDK log file '" + logFile + "': " + ex.Message + ".  See inner exception for more detail.", ex);
                }
            }

        }
        #endregion Async


    }
}
