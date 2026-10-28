using AutoMapper;
using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.Models;
using LibraryManager.ApplicationCore.Repositories;
using LibraryManager.ApplicationCore.ResultPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<BookDTO>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CreateBookHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;

        }
        public async Task<Result<BookDTO>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            var ExistingBook = await _bookRepository.GetBookByIsbnAsync(book.ISBN, cancellationToken);
            if(ExistingBook != null)
            {
                return Result<BookDTO>.Failure(Errors.BookAlreadyExists);
            }
            var createdBook = await _bookRepository.AddBookAsync(book,cancellationToken);
            return  Result<BookDTO>.Success(_mapper.Map<BookDTO>(createdBook));


        }

    }


    }

