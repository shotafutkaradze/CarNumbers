using Application.Commands.CarNumberCommands;
using Application.Commands.CarOrderCommands;
using Application.Queries.CarNumberQuerys;
using Doamin.DTOs;
using Doamin.Helper;
using Doamin.ReturnResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("numberId")]
        public async Task<ReturnResult<GetCarNumberDTO>> GetCarNumberById(int numberId)
        {

            var cartDetails = await _mediator.Send(new GetCarNumberByIdQuery() { Id = numberId });

            return cartDetails;
        }
        [HttpGet]
        public async Task<ReturnResult<List<CarNumberDTO>>> GetCarNumber([FromQuery] PageRequest pageRequest)
        {
            return await _mediator.Send(new GetCarNumbersQuery() { PageNumber = pageRequest.PageNumber, PageSize = pageRequest.PageSize });
        }

        [HttpPost("number")]
        public async Task<ReturnResult<GetCarNumberDTO>> AddCarNumber(string number)
        {
            return await _mediator.Send(new CreateCarNumberCommand() { Number = number });
        }
        [HttpPut("Id")]
        public async Task<ReturnResult<CarNumberDTO>> UpdateCarNumber(CarNumberDTO model)
        {
            return await _mediator.Send(new CarNumberUpdateCommand(model));
        }
        [HttpDelete("Id")]
        public async Task<string> DeleteCarNumber(int Id)
        {
            return await _mediator.Send(new DeleteCarNumberCommand() { Id = Id });
        }

        [HttpPost]
        public async Task<ReturnResult<GetOrderCaseDTO>> CreateOrder(SetOrderCaseDTO moddel)
        {
            return await _mediator.Send(new CreateOrderCommand() { OrderCaseDTOs = moddel });
        }
        [HttpGet("Id")]
        public async Task<string> CancelOrder(int Id)
        {
            return await _mediator.Send(new CancelOrderCommand() { Id = Id });
        }
        //[HttpPost]
        //public ActionResult<ReturnResult<CarNumberDTO>> SearchCarNumber([FromBody] CarNumberDTO searchModel, int page, int pageSize)
        //{
        //    return _mediator.Send(new SearchCarNumberCommand() { model = searchModel , page = page , pageSize = pageSize});
        //}
    }
}
