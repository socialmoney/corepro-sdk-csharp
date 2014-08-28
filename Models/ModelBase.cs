using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class ModelBase
    {
        /// <summary>
        /// RequestId automatically returned by CorePro on all responses.  Used for logging and troubleshooting purposes.
        /// </summary>
        public Guid? RequestId { get; set; }
    }
}
