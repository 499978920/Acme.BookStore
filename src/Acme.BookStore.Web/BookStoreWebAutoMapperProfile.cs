using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Web.Pages.Authors;
using Acme.BookStore.Web.Pages.Books;
using AutoMapper;

namespace Acme.BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            CreateMap<BookDto, EditBookViewModel>();
            CreateMap<CreateBookViewModel, CreateUpdateBookDto>();
            CreateMap<EditBookViewModel, CreateUpdateBookDto>();
            
            CreateMap<CreateAuthorViewModel, CreateAuthorDto>();
            CreateMap<AuthorDto, EditAuthorViewModel>();
            CreateMap<EditAuthorViewModel, UpdateAuthorDto>();
        }
    }
}
