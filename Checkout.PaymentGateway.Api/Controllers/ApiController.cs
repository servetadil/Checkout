using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected ApiController()
        {

        }
    }
}
