using LibraryManager.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Repositories
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book, CancellationToken ct = default);
        Task<Book?> GetBookByIsbnAsync(string isbn, CancellationToken ct = default);
    }
}
