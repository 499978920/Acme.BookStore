using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Shouldly;
using Xunit;

namespace Acme.BookStore
{
    public class AuthorAppServiceTests : BookStoreApplicationTestBase
    {
        private readonly IAuthorAppService _authorAppService;

        public AuthorAppServiceTests()
        {
            _authorAppService = base.GetRequiredService<IAuthorAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Authors_Without_Any_Filter()
        {
            var result = await _authorAppService.GetListAsync(new GetAuthorListDto());
            
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(x=>x.Name == "张三");
            result.Items.ShouldContain(x => x.ShortBio.IsNullOrWhiteSpace());
        }

        [Fact]
        public async Task Should_Get_Filtered_Authors()
        {
            var result = await _authorAppService.GetListAsync(new GetAuthorListDto
            {
                Filter = "张三"
            });
            
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(x => x.Name == "张三");
            result.Items.ShouldNotContain(x => x.Name == "李四");
        }

        [Fact]
        public async Task Should_Create_A_New_Author()
        {
            var authorDto = await _authorAppService.CreateAsync(
                new CreateAuthorDto
                {
                    Name = "王五",
                    BirthDate = new DateTime(1988, 3, 5),
                    ShortBio = "湖南人"
                }
            );

            authorDto.Id.ShouldNotBe(Guid.Empty);
            authorDto.Name.ShouldBe("王五");
        }

        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_Author()
        {
            await Assert.ThrowsAsync<AuthorAlreadyExistsException>(async () =>
            {
                await _authorAppService.CreateAsync(
                    new CreateAuthorDto()
                    {
                        Name = "张三",
                        BirthDate = DateTime.Now,
                        ShortBio = "nothing"
                    }
                );
            });
        }
    }
}