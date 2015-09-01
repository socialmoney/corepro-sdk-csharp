using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    /// <summary>
    /// Convenience class for implementing ILogWriter.  All functions do nothing and do not throw exceptions.
    /// </summary>
    public class LogWriterBase : ILogWriter
    {
        public virtual void Write(DateTimeOffset timestamp, int managedThreadId, string message, object userDefinedObject)
        {
            // do nothing
        }

        public virtual void Write(DateTimeOffset timestamp, int managedThreadId, System.Net.HttpWebRequest request, string body, object userDefinedObject)
        {
            // do nothing
        }

        public virtual void Write(DateTimeOffset timestamp, int managedThreadId, System.Net.HttpWebResponse response, string body, object userDefinedObject)
        {
            // do nothing
        }

        public virtual void Write(DateTimeOffset timestamp, int managedThreadId, Exception exception, string additionalInfo, object userDefinedObject)
        {
            // do nothing
        }

        public async virtual Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, string message, object userDefinedObject)
        {
            // do nothing
            await Task.FromResult<object>(null);
        }

        public async virtual Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, System.Net.HttpWebRequest request, string body, object userDefinedObject)
        {
            // do nothing
            await Task.FromResult<object>(null);
        }

        public async virtual Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, System.Net.HttpWebResponse response, string body, object userDefinedObject)
        {
            // do nothing
            await Task.FromResult<object>(null);
        }

        public async virtual Task WriteAsync(CancellationToken cancellationToken, DateTimeOffset timestamp, int managedThreadId, Exception exception, string additionalInfo, object userDefinedObject)
        {
            // do nothing
            await Task.FromResult<object>(null);
        }

    }
}
