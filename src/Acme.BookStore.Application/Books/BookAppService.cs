using System;
using Acme.BookStore.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books
{
    public class BookAppService : CrudAppService<
            Book,
            BookDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto>,
        IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository) : base(repository)
        {
            base.GetPolicyName = BookStorePermissions.Books.Default;
            base.GetListPolicyName = BookStorePermissions.Books.Default;
            base.CreatePolicyName = BookStorePermissions.Books.Create;
            base.UpdatePolicyName = BookStorePermissions.Books.Edit;
            base.DeletePolicyName = BookStorePermissions.Books.Delete;
        }
    }
}