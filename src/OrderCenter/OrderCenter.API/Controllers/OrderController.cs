using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderCenter.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace OrderCenter.API.Controllers
{
    [Authorize(Roles = "student")]
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IMediator _Mediator;

        public OrderController(IMediator Mediator)
        {
            _Mediator = Mediator;
        }

        [Route("Create")]
        [HttpPost]
        public async  Task<IActionResult> Create(CreateOrderCommand command)
        {
            var response = await _Mediator.Send(command);
            return Json(response);
        }

        [Route("Close")]
        [HttpPost]
        public async Task<IActionResult> Close(ClosedOrderCommand command)
        {
            var response = await  _Mediator.Send(command);
            return Json(response);
        }

    }
}
