using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class CustomerVerifyRequest : ModelBase
    {
        public int? CustomerId { get; set; }
        public string VerificationId { get; set; }
        public List<CustomerAnswer> Answers { get; set; }

        public CustomerVerifyRequest()
        {
            Answers = new List<CustomerAnswer>();
        }

    }
}
