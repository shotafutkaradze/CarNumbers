using Application.Queries.CarNumberQuerys;
using AutoMapper;
using Doamin.DTOs;
using Doamin.Helper;
using Doamin.Repositories.Interface;
using Doamin.ReturnResult;
using MediatR;

namespace Application.QueriesHandlers
{
    public class GetCarNumberListQueryHandler : IRequestHandler<GetCarNumbersQuery, ReturnResult<List<CarNumberDTO>>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public GetCarNumberListQueryHandler(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ReturnResult<List<CarNumberDTO>>> Handle(GetCarNumbersQuery query, CancellationToken cancellationToken)
        {
            var result = new ReturnResult<List<CarNumberDTO>>();
            try
            {
                var res = await _unitofwork.CarNumber.GetAllPageing(null,new PageRequest { PageSize = query.PageSize,PageNumber= query.PageNumber}, null);
                if (res.Count == 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = $"Car is Empty";

                    return result;
                }
           
                result.Result = _mapper.Map<List<CarNumberDTO>>(res);
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
