using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Entities
{
    public class CarNumbers : EntityBase
    {
        public string Number { get; set; }
        public ICollection<OrderCase> OrderCases { get; set; }
    }
}
