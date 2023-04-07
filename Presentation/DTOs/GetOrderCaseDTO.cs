using Doamin.Enam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.DTOs
{
    public class GetOrderCaseDTO
    {
        public int Id { get; set; }
        public BuyType Type { get; set; }
        public string? CancelDate { get; set; }
        public string? EndDate { get; set; }
        public int CarNumbersId { get; set; }

        public CarNumberDTO CarNumber { get; set; }
    }
}
