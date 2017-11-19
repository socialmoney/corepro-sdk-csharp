using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CorePro.SDK.Models;

namespace CorePro.SDK.Utils
{
    public class Stats
    {
        private static Dictionary<int, UrlStats> __lastRequested = new Dictionary<int, UrlStats>();
        private static Dictionary<string, UrlStats> __perUrlStats = new Dictionary<string, UrlStats>();


        public static void ClearUrlStats()
        {
            __perUrlStats = new Dictionary<string, UrlStats>();
        }

        private static UrlStats recordUrlStats<T>(Uri uri, double duration, Envelope<T> envelope)
        {
            var key = uri.AbsolutePath;

            UrlStats stat = null;
            if (__perUrlStats.TryGetValue(key, out stat))
            {
                stat.TotalRequestCount += 1;
                stat.TotalRequestDuration += duration;
                stat.LastRequestDuration = duration;

                stat.AverageRequestDuration = Math.Round(stat.TotalRequestDuration / (double)stat.TotalRequestCount, 3);

                if (duration < stat.MinimumRequestDuration)
                    stat.MinimumRequestDuration = duration;
                if (duration > stat.MaxiumRequestDuration)
                    stat.MaxiumRequestDuration = duration;

            }
            else
            {
                stat = new UrlStats();
                stat.TotalRequestCount = 1;
                stat.TotalRequestDuration = duration;
                stat.LastRequestDuration = duration;

                stat.AverageRequestDuration = duration;

                stat.MaxiumRequestDuration = duration;
                stat.MinimumRequestDuration = duration;

            }

            if (envelope != null)
            {
                stat.LastRequestBody = envelope.RawRequestBody;
                stat.LastResponseBody = envelope.RawResponseBody;
            }
            else
            {
                stat.LastRequestBody = "-";
                stat.LastResponseBody = "-";
            }

            __perUrlStats[key] = stat;

            return stat;

        }

        public static Dictionary<string, UrlStats> GetUrlStats()
        {
            return __perUrlStats;
        }

        /// <summary>
        /// Retrieves the UrlStats for the last request on the given thread.  If threadid is unspecified or 0, defaults to the current managed thread id.
        /// </summary>
        /// <returns></returns>
        public static UrlStats UrlStatsForLastRequest(int threadId = 0)
        {
            if (threadId == 0)
                threadId = Thread.CurrentThread.ManagedThreadId;

            UrlStats rv = null;
            if (__lastRequested.TryGetValue(threadId, out rv))
                return rv;
            return null;
        }


        /// <summary>
        /// Will record statistics for the given request after it has been issued.  Use Stats.GetUrlStats(url) for url-specific information or Stats.LastResponseDuration (which is thread-sensitive) to pull the duration of the last request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        internal static T Record<T>(Func<Envelope<T>> callback, HttpWebRequest req) 
        {
            long start = DateTime.Now.Ticks;
            Envelope<T> rv = null;
            try
            {
                rv = callback();
                if (rv != null)
                {
                    return rv.Data;
                }
                else
                {
                    return default(T);
                }
            }
            finally
            {
                if (req != null)
                {
                    var end = DateTime.Now.Ticks;
                    var duration = Math.Round(new TimeSpan(end - start).TotalMilliseconds, 2);
                    var stat = recordUrlStats(req.RequestUri, duration, rv);
                    __lastRequested[Thread.CurrentThread.ManagedThreadId] = stat;
                }
            }
        }

        internal async static Task<Envelope<T>> RecordAsync<T>(CancellationToken cancellationToken, Func<Task<Envelope<T>>> callback, HttpWebRequest req)
        {
            long start = DateTime.Now.Ticks;
            Envelope<T> rv = null;
            try
            {
                rv = await callback();
                return rv;
            }
            finally
            {
                if (req != null)
                {
                    var end = DateTime.Now.Ticks;
                    var duration = Math.Round(new TimeSpan(end - start).TotalMilliseconds, 2);
                    var stat = recordUrlStats(req.RequestUri, duration, rv);
                    __lastRequested[Thread.CurrentThread.ManagedThreadId] = stat;
                }
            }
        }
    }

}
