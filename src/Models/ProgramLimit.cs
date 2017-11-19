using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Models
{
    public class ProgramLimit
    {
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public override string ToString()
        {
            return "min: " + MinimumAmount + ", max: " + MaximumAmount;
        }
    }
}
