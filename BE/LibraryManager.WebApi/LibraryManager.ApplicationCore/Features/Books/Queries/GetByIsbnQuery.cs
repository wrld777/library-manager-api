using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.ResultPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Queries
{
    public record GetByIsbnQuery(string Isbn ) : IRequest<Result<BookDTO>>;
 
}
