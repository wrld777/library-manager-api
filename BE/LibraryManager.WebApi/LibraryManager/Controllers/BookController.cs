using LibraryManager.ApplicationCore.Features.Books.Commands;
using LibraryManager.FluentValidator;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ISender _sender;

        public BookController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookCommand command, CancellationToken ct = default)
        {
            var response = await _sender.Send(command, ct);
            return Ok(response);
        }

    }
}
