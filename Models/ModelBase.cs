using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class ModelBase
    {

        public ModelBase()
        {
        }

        public ModelBase(RequestMetaData metaData)
        {
            _metaData = metaData;
        }

        /// <summary>
        /// RequestId automatically returned by CorePro on all responses.  Used for logging and troubleshooting purposes.
        /// </summary>
        public Guid? RequestId { get; set; }

        /// <summary>
        /// Gets or sets a user-defined object for third-party code to pass values through this SDK.
        /// </summary>
        public object UserDefined { get; set; }

        private RequestMetaData _metaData;
        /// <summary>
        /// Gets or sets metadata about the customer making the request.  e.g. Latitude, longitude, IP Address, User Agent, etc.
        /// </summary>
        public RequestMetaData MetaData
        {
            get
            {
                if (_metaData == null)
                {
                    _metaData = new RequestMetaData();
                }
                return _metaData;
            }
            set
            {
                _metaData = value;
            }
        }

    }
}
