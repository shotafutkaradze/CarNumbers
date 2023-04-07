using Application.Commands;
using Application.Commands.CarOrderCommands;
using AutoMapper;
using Doamin.DTOs;
using Doamin.Enam;
using Doamin.Entities;
using Doamin.Helper;
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
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ReturnResult<GetOrderCaseDTO>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ReturnResult<GetOrderCaseDTO>> Handle(CreateOrderCommand query, CancellationToken cancellationToken)
        {
            var result = new ReturnResult<GetOrderCaseDTO>();
            try
            {
                var data = new OrderCase
                {
                    CarNumbersId = query.OrderCaseDTOs.CarNumbersId,
                    CancelDate = null,
                    CreatedOn = DateTime.Now,
                    EndDate = query.OrderCaseDTOs.Type == BuyType.Armor ? DateTime.Now.AddDays(10) : null,
                    Type = query.OrderCaseDTOs.Type,
                };
                data.carNumbers = await _unitofwork.CarNumber.GetById(query.OrderCaseDTOs.CarNumbersId);
                await _unitofwork.OrderCases.Add(data);
                var res = await _unitofwork.Save();


                if (res > 0)
                {
                    result.Result = _mapper.Map<GetOrderCaseDTO>(data);
                    result.IsSuccess = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
