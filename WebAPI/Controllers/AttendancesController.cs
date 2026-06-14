using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Attendances.Commands;
using Business.Handlers.Attendances.Queries;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AttendancesController : BaseApiController
    {
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Attendance>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetAttendancesQuery()));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Attendance>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetMemberAttendances(int memberId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetMemberAttendancesQuery { MemberId = memberId }));
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] CreateAttendanceCommand command)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(command));
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("{id}/checkout")]
        public async Task<IActionResult> CheckOut(int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new CheckOutCommand { AttendanceId = id }));
        }
    }
}
