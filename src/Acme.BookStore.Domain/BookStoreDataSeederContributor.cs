using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() > 0)
            {
                return;
            }

            await _bookRepository.InsertAsync(new Book()
                {
                    Name = "DDD",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(2021, 08, 01),
                    Price = 109.99f,
                },
                autoSave: true);

            await _bookRepository.InsertAsync(new Book()
                {
                    Name = "实现领域驱动设计",
                    Type = BookType.Adventure,
                    PublishDate = new DateTime(2020, 04, 16),
                    Price = 79.59f,
                },
                autoSave: true);
        }
    }
}