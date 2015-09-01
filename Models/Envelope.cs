using CorePro.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class Envelope<T>
    {
        public Envelope()
        {
            Errors = new List<Error>();
        }
        public T Data { get; set; }
        public string RawRequestBody { get; set; }
        public string RawResponseBody { get; set; }
        public List<Error> Errors { get; private set; }
        public int Status { get; set; }
        public Guid? RequestId { get; set; }

        public string ToJson()
        {
            return new JsonSerializer().Serialize(this);
        }
    }
}
