using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.DTOs
{
    public  class CarNumberDTO
    {
        public int id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Number { get; set; }
    }
}
