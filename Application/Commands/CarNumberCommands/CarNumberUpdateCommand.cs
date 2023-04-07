using Amazon.Runtime.Internal;
using Doamin.DTOs;
using Doamin.ReturnResult;
using MediatR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CarNumberCommands
{
    public class CarNumberUpdateCommand : IRequest<ReturnResult<CarNumberDTO>>
    {
        public CarNumberDTO model { get; set; }
        public CarNumberUpdateCommand(CarNumberDTO model)
        {
            this.model = model;
        }
    }
}
