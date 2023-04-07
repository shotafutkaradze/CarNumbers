using Doamin.DTOs;
using Doamin.Enam;
using Doamin.ReturnResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CarOrderCommands
{
    public class CancelOrderCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
