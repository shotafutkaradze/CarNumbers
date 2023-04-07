using Doamin.DTOs;
using Doamin.Helper;
using Doamin.ReturnResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CarNumberQuerys
{
    public class GetCarNumbersQuery : IRequest<ReturnResult<List<CarNumberDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
