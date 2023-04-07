using Doamin.DTOs;
using Doamin.ReturnResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CarNumberQuerys
{
    public class GetCarNumberByIdQuery : IRequest<ReturnResult<GetCarNumberDTO>>
    {
        public int Id { get; set; }

    }
}
