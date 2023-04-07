using Application.Commands.CarNumberCommands;
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
    public class CarNumberUpdateHandler : IRequestHandler<CarNumberUpdateCommand, ReturnResult<CarNumberDTO>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public CarNumberUpdateHandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ReturnResult<CarNumberDTO>> Handle(CarNumberUpdateCommand query, CancellationToken cancellationToken)
        {
            var result = new ReturnResult<CarNumberDTO>();
            try
            {
                var temp = await _unitofwork.CarNumber.GetById(query.model.id);
                if (temp == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"CarNumber Not Found";

                    return result;
                }
                var regexnumber = NumberRegex.CarNumberRegex(query.model.Number);
                if (!regexnumber)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"The format is invalid";

                    return result;
                }
                var chechNumber = await _unitofwork.CarNumber.Find(x => x.Number == query.model.Number);
                if (chechNumber.Count() != 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"This number: {query.model.Number} exists";

                    return result;
                }
                temp.Number = query.model.Number;
                temp.CreatedOn = query.model.CreatedOn ?? temp.CreatedOn;

                await _unitofwork.CarNumber.Update(temp);
                var res = await _unitofwork.Save();


                if (res > 0)
                {
                    result.Result = _mapper.Map<CarNumberDTO>(temp);
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
