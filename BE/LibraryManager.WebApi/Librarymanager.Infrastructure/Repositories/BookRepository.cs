using Librarymanager.Infrastructure.context;
using LibraryManager.ApplicationCore.Generics;
using LibraryManager.ApplicationCore.Models;
using LibraryManager.ApplicationCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librarymanager.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LmDbContext _context;

        public BookRepository(LmDbContext context)
        {
            _context = context;

        }

        public async Task<PagedList<Book>> GetAllPaged(int page, int pageSize, CancellationToken ct)
        {
            var books = _context.Books.AsNoTracking();
            var totalCount = await books.CountAsync(ct);

            var pagedBooks = await books
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PagedList<Book>(pagedBooks, page, pageSize, totalCount);
        }

        public async Task<Book?> GetBookByIsbnAsync(string isbn, CancellationToken ct = default)
        {
            return await _context.Books.FindAsync(isbn, ct);
        }

        public async Task<Book> AddBookAsync(Book book, CancellationToken ct = default)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(ct);
            return book;
        }
    }
}
