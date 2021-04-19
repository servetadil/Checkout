using System.Threading.Tasks;
using Checkout.PaymentGateway.Application.Authentication.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.PaymentGateway.Api.Controllers
{
    [ApiController]
    public class AuthenticateController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePayment([FromBody] AuthenticateUserCommand model)
        {
            var token = await _mediator.Send(model);

            return Ok(new { Token = token });
        }
    }
}
