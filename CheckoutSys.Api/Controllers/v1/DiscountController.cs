using CheckoutSys.API.Controllers;
using CheckoutSys.Application.Features.Discounts.Commands.Create;
using CheckoutSys.Application.Features.Discounts.Commands.Delete;
using CheckoutSys.Application.Features.Discounts.Commands.Update;
using CheckoutSys.Application.Features.Discounts.Queries.GetAllPaged;
using CheckoutSys.Application.Features.Discounts.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CheckoutSys.Api.Controllers.v1
{
    public class DiscountController : BaseApiController<DiscountController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var products = await _mediator.Send(new GetAllDiscountsQuery(pageNumber, pageSize));
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _mediator.Send(new GetDiscountByIdQuery() { Id = id });
            return Ok(product);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateDiscountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateDiscountCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDiscountCommand { Id = id }));
        }
    }
}