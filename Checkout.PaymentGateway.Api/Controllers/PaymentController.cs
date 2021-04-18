using System.Threading.Tasks;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.PaymentGateway.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IMediator mediator,
            ILogger<PaymentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("create-payment")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand model)
        {
            var orderId = await _mediator.Send(model);

            return Ok(new { OrderId = orderId });
        }
    }
}
