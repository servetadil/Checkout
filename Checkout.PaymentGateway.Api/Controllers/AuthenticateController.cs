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
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(
            IMediator mediator,
            ILogger<AuthenticateController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
