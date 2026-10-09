using LibraryManager.ApplicationCore.Features.Ping;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LibraryManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly ISender _sender;

        public PingController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string message = "hello", CancellationToken ct = default)
        {
            var response = await _sender.Send(new PingQuery(message), ct);
            return Ok(response);
        }
    }
}
