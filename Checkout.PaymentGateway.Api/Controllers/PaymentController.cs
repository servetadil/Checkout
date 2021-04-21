﻿using System.Threading.Tasks;
using Checkout.PaymentGateway.Application.Payments.Commands.CreatePayment;
using Checkout.PaymentGateway.Application.Payments.Commands.SubmitPayment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.PaymentGateway.Api.Controllers
{
    [ApiController]
    public class PaymentController : ApiController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create empty payments for merchants
        /// </summary>
        /// 
        [Authorize]
        [HttpPost]
        [Route("create-payment")]
        [Produces("application/json")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand model)
        {
            var orderId = await _mediator.Send(model);

            return Ok(orderId);
        }

        /// <summary>
        /// Submit and send to the Bank for process payment
        /// </summary>
        /// 
        [Authorize]
        [HttpPost]
        [Route("submit-payment")]
        [Produces("application/json")]
        public async Task<IActionResult> SubmitPayment([FromBody] SubmitPaymentCommand model)
        {
            var result = await _mediator.Send(model);

            return Ok(result);
        }
    }
}
