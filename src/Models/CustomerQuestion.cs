using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class CustomerQuestion
    {
        public List<string> Answers { get; set; }
        public string Prompt { get; set; }
        public string Type { get; set; }

        public CustomerQuestion()
        {
            Answers = new List<string>();
        }
    }
}
