using Doamin.DTOs;
using Doamin.ReturnResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CarNumberCommands
{
    public class CreateCarNumberCommand : IRequest<ReturnResult<GetCarNumberDTO>>
    {
        public string Number { get; set; }
    }
}
