using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class Error
    {
        public int Code { get; set;  }
        public string Message { get; set; }

        public override string ToString()
        {
            return Code + ":" + Message;
        }
    }
}
