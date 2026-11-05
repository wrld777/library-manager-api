using AutoMapper;
using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.Repositories;
using LibraryManager.ApplicationCore.ResultPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Queries
{
    public class GetByIsbnHandler : IRequestHandler<GetByIsbnQuery, Result<BookDTO>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetByIsbnHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            
        }

        public async Task<Result<BookDTO>> Handle(GetByIsbnQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByIsbnAsync(request.Isbn, cancellationToken);
            if(book == null)
            {
                return Result<BookDTO>.Failure(Errors.BookNotFound);
            }

            return Result<BookDTO>.Success(_mapper.Map<BookDTO>(book));
        }
    }
}
