using AutoMapper;
using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.Models;
using LibraryManager.ApplicationCore.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDTO>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CreateBookHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;

        }
        public async Task<BookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);
            var createdBook = await _bookRepository.AddBookAsync(book,cancellationToken);
            return _mapper.Map<BookDTO>(createdBook);


        }

    }


    }

