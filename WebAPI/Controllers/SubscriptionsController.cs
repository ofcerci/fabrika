using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Subscriptions.Commands;
using Business.Handlers.Subscriptions.Queries;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SubscriptionsController : BaseApiController
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Subscription>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetSubscriptionsQuery()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Subscription))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("member/{memberId}/active")]
        public async Task<IActionResult> GetActiveMemberSubscription(int memberId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetMemberActiveSubscriptionQuery { MemberId = memberId }));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSubscriptionCommand command)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(command));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("freeze")]
        public async Task<IActionResult> Freeze([FromBody] FreezeSubscriptionCommand command)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(command));
        }
    }
}
