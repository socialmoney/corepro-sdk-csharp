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

        /// <summary>
        /// Gets or sets a user-defined object for third-party code to pass values through this SDK.
        /// </summary>
        public object UserDefined { get; set; }

    }
}
