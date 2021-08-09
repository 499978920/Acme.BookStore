using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Acme.BookStore
{
    public class BookAppServiceTests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IAuthorAppService _authorAppService;

        public BookAppServiceTests()
        {
            _authorAppService = base.GetRequiredService<IAuthorAppService>();
            _bookAppService = base.GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            // act
            var result = await _bookAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
            );

            // assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(x => x.Name == "DDD"
                                            && x.AuthorName == "张三");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            var authors = await _authorAppService.GetListAsync(new GetAuthorListDto());
            var firstAuthor = authors.Items.First();
            
            // act
            var result = await _bookAppService.CreateAsync(
                new CreateUpdateBookDto()
                {
                    AuthorId = firstAuthor.Id,
                    Name = "Test Book",
                    Price = 10,
                    PublishDate = DateTime.Now,
                    Type = BookType.Adventure
                }
            );

            // assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("Test Book");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(
                    new CreateUpdateBookDto()
                    {
                        Name = "",
                        Price = 10,
                        PublishDate = DateTime.Now,
                        Type = BookType.Adventure
                    }
                );
            });

            // assert
            exception.ValidationErrors
                .ShouldContain(err => err.MemberNames.Any(x => x == "Name"));
        }
    }
}