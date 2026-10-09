using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Ping
{
    public record PingQuery(string message = "Hello") : IRequest<string>;

}
