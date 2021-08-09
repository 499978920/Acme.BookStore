using System;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
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
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public BookStoreDataSeederContributor(
            IRepository<Book, Guid> bookRepository,
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() > 0)
            {
                return;
            }

            var author1 = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "张三",
                    new DateTime(1999, 02, 05),
                    "巴拉巴拉")
            );

            var author2 = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "李四",
                    new DateTime(2000, 10, 2)
                )
            );

            await _bookRepository.InsertAsync(new Book()
                {
                    AuthorId = author1.Id,
                    Name = "DDD",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(2021, 08, 01),
                    Price = 109.99f,
                },
                autoSave: true);

            await _bookRepository.InsertAsync(new Book()
                {
                    AuthorId = author2.Id,
                    Name = "实现领域驱动设计",
                    Type = BookType.Adventure,
                    PublishDate = new DateTime(2020, 04, 16),
                    Price = 79.59f,
                },
                autoSave: true);
        }
    }
}