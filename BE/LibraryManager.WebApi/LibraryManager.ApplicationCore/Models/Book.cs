using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Models
{
    public class Book
    {
        string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int CopiesAvailable { get; set; }
    }
}
