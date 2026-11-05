using AutoMapper;
using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.Generics;
using LibraryManager.ApplicationCore.Repositories;
using LibraryManager.ApplicationCore.ResultPattern;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Features.Books.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, Result<PagedList<BookDTO>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;

        }

        public async Task<Result<PagedList<BookDTO>>> Handle(GetAllQuery query, CancellationToken ct)
        {
            var page = query.page ?? 1;
            var pageSize = query.pageSize ?? 1;

            var books = await _bookRepository.GetAllPaged(page, pageSize, ct);

            if (!books.Items.Any())
            {
                return Result<PagedList<BookDTO>>.Failure(Errors.EmptyList);
            }
            var dtos = _mapper.Map<List<BookDTO>>(books.Items);

            return Result<PagedList<BookDTO>>.Success(new PagedList<BookDTO>(dtos, books.page, books.pageSize, books.totalCount));
        }
    }
}
