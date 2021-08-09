using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Web.Pages.Authors;
using AutoMapper;

namespace Acme.BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            CreateMap<BookDto, CreateUpdateBookDto>();
            
            CreateMap<CreateAuthorViewModel, CreateAuthorDto>();
            CreateMap<AuthorDto, EditAuthorViewModel>();
            CreateMap<EditAuthorViewModel, UpdateAuthorDto>();
        }
    }
}
