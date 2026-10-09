using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Ping
{
    public class PingQueryHandler : IRequestHandler<PingQuery, string>
    {
        public Task<string> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"Pong: {request.message}");
        }
    }
  
}
