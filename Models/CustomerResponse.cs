using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class CustomerResponse : ModelBase
    {
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<CustomerMessage> Messages { get; set; }
        public List<CustomerQuestion> Questions { get; set; }
        public string VerificationId { get; set; }
        public string VerificationStatus { get; set; }

        public CustomerResponse()
        {
            Messages = new List<CustomerMessage>();
            Questions = new List<CustomerQuestion>();
        }
    }
}
