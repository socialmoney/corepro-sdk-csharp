using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class FileContent
    {
        private byte[] _rawContent;
        public byte[] RawContent
        {
            get
            {
                return _rawContent;
            }
            set
            {
                if (value == null)
                    _content = null;
                else
                    _content = Convert.ToBase64String(value);
            }
        }

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                if (value == null)
                    _rawContent = null;
                else
                    _rawContent = Convert.FromBase64String(value);
            }
        }

        public string ContentType { get; set; }
        public long ContentLength { get; set; }
    }
}
