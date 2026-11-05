using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.Generics;
using LibraryManager.ApplicationCore.ResultPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Queries
{
    public record GetAllQuery(int? page, int? pageSize) : IRequest<Result<PagedList<BookDTO>>>;

}
