using Application.Commands.CarNumberCommands;
using Application.Queries;
using AutoMapper;
using Doamin.DTOs;
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

namespace Application.CommandHandlers.CarNumberHandlers
{
    public class CreateCarNumberHeandler : IRequestHandler<CreateCarNumberCommand, ReturnResult<GetCarNumberDTO>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public CreateCarNumberHeandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ReturnResult<GetCarNumberDTO>> Handle(CreateCarNumberCommand query, CancellationToken cancellationToken)
        {
            var result = new ReturnResult<GetCarNumberDTO>();
            try
            {
                var prc = GetNumberPrice.CalPrice(query.Number);

                var regexnumber = NumberRegex.CarNumberRegex(query.Number);
                if (!regexnumber)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"The format is invalid";

                    return result;
                }
                var chechNumber = await _unitofwork.CarNumber.Find(x => x.Number == query.Number);
                if (chechNumber.Count() != 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"This number: {query.Number} exists";

                    return result;
                }
                var data = new CarNumbers
                {
                    CreatedOn = DateTime.Now,
                    Number = query.Number,
                };

                await _unitofwork.CarNumber.Add(data);
                var res = await _unitofwork.Save();

                var tempdata = _mapper.Map<GetCarNumberDTO>(data);
                tempdata.Price = prc;
                if (res > 0)
                {
                    result.Result = tempdata;
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
