using AutoMapper;
using LibraryManager.ApplicationCore.Dto_s;
using LibraryManager.ApplicationCore.Features.Books.Commands;
using LibraryManager.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.ApplicationCore.Automapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookCommand, Book>().ReverseMap();
            CreateMap< Book, BookDTO>().ReverseMap();
        }
    }
}
