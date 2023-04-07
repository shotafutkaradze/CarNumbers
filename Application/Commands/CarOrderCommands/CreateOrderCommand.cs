using Doamin.DTOs;
using Doamin.ReturnResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CarOrderCommands
{
    public class CreateOrderCommand : IRequest<ReturnResult<GetOrderCaseDTO>>
    {
        public SetOrderCaseDTO OrderCaseDTOs { get; set; }
        public int MyProperty { get; set; }
    }
}
