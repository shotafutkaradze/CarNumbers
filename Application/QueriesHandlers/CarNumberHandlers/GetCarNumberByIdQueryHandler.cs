using Application.Queries.CarNumberQuerys;
using AutoMapper;
using Doamin.DTOs;
using Doamin.Helper;
using Doamin.Repositories.Interface;
using Doamin.ReturnResult;
using MediatR;

namespace Application.QueriesHandlers
{
    public class GetCarNumberByIdQueryHandler : IRequestHandler<GetCarNumberByIdQuery, ReturnResult<GetCarNumberDTO>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public GetCarNumberByIdQueryHandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ReturnResult<GetCarNumberDTO>> Handle(GetCarNumberByIdQuery query, CancellationToken cancellationToken)
        {
            var result = new ReturnResult<GetCarNumberDTO>();

            try
            {
                var res = await _unitofwork.CarNumber.GetById(query.Id);

                if (res == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"There is no record for this ID={query.Id}";

                    return result;
                }
                var tempdata = _mapper.Map<GetCarNumberDTO>(res);
                tempdata.Price = GetNumberPrice.CalPrice(tempdata.Number);

                result.Result = tempdata;
                result.IsSuccess = true;
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
