using BookLendingService.Application.Books.Handlers;
using BookLendingService.Application.Books.Queries;
using FluentAssertions;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class GetAllBooksHandlerTests : InMemoryBookTestBase
    {
        private GetAllBooksHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new GetAllBooksHandler(Repo);
        }

        [Test]
        public async Task Should_Return_All_Books()
        {
            var result = await _handler.Handle(new GetAllBooksQuery(), default);

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.First().Title.Should().Be(SampleBook.Title);
        }
    }
}
