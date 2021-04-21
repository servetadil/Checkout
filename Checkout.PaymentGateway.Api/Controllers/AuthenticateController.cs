using System.Threading.Tasks;
using Checkout.PaymentGateway.Application.Authentication.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        ///  Authenticate with MerchantID and ApiKey
        /// </summary>
        /// 
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        [Produces("application/json")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand model)
        {
            var authUser = await _mediator.Send(model);

            return Ok(authUser);
        }
    }
}
