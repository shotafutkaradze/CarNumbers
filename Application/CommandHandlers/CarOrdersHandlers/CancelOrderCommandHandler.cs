using Application.Commands.CarOrderCommands;
using AutoMapper;
using Doamin.DTOs;
using Doamin.Enam;
using Doamin.Entities;
using Doamin.Repositories.Interface;
using Doamin.ReturnResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers.CarOrdersHandlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, string>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public CancelOrderCommandHandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<string> Handle(CancelOrderCommand query, CancellationToken cancellationToken)
        {
            var result = "Successfully executed!";
            try
            {
                var temp = await _unitofwork.OrderCases.GetById(query.Id);

                temp.CancelDate = DateTime.Now;

                await _unitofwork.OrderCases.Update(temp);
                var res = await _unitofwork.Save();


                if (res > 0)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = "An error occurred!";
            }
            return result;
        }
    }
}
