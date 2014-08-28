using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    public class UrlStats
    {
        public int TotalRequestCount { get; set; }
        public double TotalRequestDuration { get; set; }
        public double AverageRequestDuration { get; set; }
        public double MaxiumRequestDuration { get; set; }
        public double MinimumRequestDuration { get; set; }
        public double LastRequestDuration { get; set; }
        public string LastRequestBody { get; set; }
        public string LastResponseBody { get; set; }
    }
}
