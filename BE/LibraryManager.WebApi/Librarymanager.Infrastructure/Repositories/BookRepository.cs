using Librarymanager.Infrastructure.context;
using LibraryManager.ApplicationCore.Models;
using LibraryManager.ApplicationCore.Repositories;
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

        public async Task<Book> AddBookAsync(Book book, CancellationToken ct = default)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(ct);
            return book;
        }
    }
}
