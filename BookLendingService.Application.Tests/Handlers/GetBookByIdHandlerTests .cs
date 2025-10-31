using BookLendingService.Application.Books.Handlers;
using BookLendingService.Application.Books.Queries;
using FluentAssertions;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class GetBookByIdHandlerTests : InMemoryBookTestBase
    {
        private GetBookByIdHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new GetBookByIdHandler(Repo);
        }

        [Test]
        public async Task Should_Return_Book_By_Id()
        {
            var result = await _handler.Handle(new GetBookByIdQuery(SampleBook.Id), default);

            result.Should().NotBeNull();
            result.Id.Should().Be(SampleBook.Id);
        }

        [Test]
        public async Task Should_Return_Null_For_Invalid_Id()
        {
            var result = await _handler.Handle(new GetBookByIdQuery(Guid.NewGuid()), default);
            result.Should().BeNull();
        }
    }
}
