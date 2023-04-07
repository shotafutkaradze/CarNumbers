using Doamin.Enam;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Entities
{
    public class OrderCase : EntityBase
    {

        [EnumDataType(typeof(BuyType))]
        public BuyType Type { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CarNumbers))]
        public int CarNumbersId { get; set; }
        public CarNumbers carNumbers { get; set; }
    }
}

