using Application.Commands;
using Application.Commands.CarNumberCommands;
using AutoMapper;
using Doamin.Repositories.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers.CarNumberHandlers
{
    public class DeleteCarNumberCommandHandler : IRequestHandler<DeleteCarNumberCommand, string>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public DeleteCarNumberCommandHandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<string> Handle(DeleteCarNumberCommand query, CancellationToken cancellationToken)
        {
            var result = "Successfully executed!";
            try
            {
                var temp = await _unitofwork.CarNumber.GetById(query.Id);
                if (temp == null)
                {
                    result = $"CarNumber Not Found";
                    return result;
                }

                await _unitofwork.CarNumber.Remove(temp);
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
