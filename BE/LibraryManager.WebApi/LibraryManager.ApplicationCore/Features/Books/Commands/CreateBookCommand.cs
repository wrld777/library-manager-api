using LibraryManager.ApplicationCore.Dto_s;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Commands
{
    public record CreateBookCommand(string Title, string Author, string ISBN, DateTime PublishedDate, int CopiesAvailable) : IRequest<BookDTO>;
}
