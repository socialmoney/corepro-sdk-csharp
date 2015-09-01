using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    public interface ILogWriter
    {
        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(string) is invoked.
        /// </summary>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="message">Message to log</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        void Write(DateTimeOffset timestamp, int managedThreadId, string message, object userDefinedObject);

        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(request) is invoked.
        /// </summary>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="request">The HttpWebRequest object of the request.</param>
        /// <param name="body">The raw body of the request.</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        void Write(DateTimeOffset timestamp, int managedThreadId, HttpWebRequest request, string body, object userDefinedObject);

        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(response) is invoked.
        /// </summary>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="response">The HttpWebRequest object of the response.</param>
        /// <param name="body">The raw body of the response.</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        void Write(DateTimeOffset timestamp, int managedThreadId, HttpWebResponse response, string body, object userDefinedObject);

        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(exception) is invoked.
        /// </summary>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="exception">The exception which caused this log call.</param>
        /// <param name="additionalInfo">Additional information provided at time of the log call.</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        void Write(DateTimeOffset timestamp, int managedThreadId, Exception exception, string additionalInfo, object userDefinedObject);





        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(string) is invoked.
        /// </summary>
        /// <param name="token">CancellationToken to handle cancelling the asynchronous write</param>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="message">Message to log</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, string message, object userDefinedObject);

        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(request) is invoked.
        /// </summary>
        /// <param name="token">CancellationToken to handle cancelling the asynchronous write</param>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="request">The HttpWebRequest object of the request.</param>
        /// <param name="body">The raw body of the request.</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, HttpWebRequest request, string body, object userDefinedObject);

        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(response) is invoked.
        /// </summary>
        /// <param name="token">CancellationToken to handle cancelling the asynchronous write</param>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="response">The HttpWebRequest object of the response.</param>
        /// <param name="body">The raw body of the response.</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, HttpWebResponse response, string body, object userDefinedObject);

        /// <summary>
        /// Called whenever CorePro.SDK.Util.Logger.Write(exception) is invoked.
        /// </summary>
        /// <param name="token">CancellationToken to handle cancelling the asynchronous write</param>
        /// <param name="timestamp">Exact time at which item is logged.</param>
        /// <param name="managedThreadId">Managed thread id of item when logging was called</param>
        /// <param name="exception">The exception which caused this log call.</param>
        /// <param name="additionalInfo">Additional information provided at time of the log call.</param>
        /// <param name="userDefinedObject">Unused by CorePro.  Provided so user code can pass an object through the CorePro logging framework.</param>
        Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, Exception exception, string additionalInfo, object userDefinedObject);
    }
}
