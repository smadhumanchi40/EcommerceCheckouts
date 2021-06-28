using CheckoutSys.API.Controllers;
using CheckoutSys.Application.Features.Orders.Queries.GetCartItemsTotal;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CheckoutSys.Api.Controllers.v1
{
    public class CheckoutController : BaseApiController<CheckoutController>
    {

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(GetCartItemsTotalQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}