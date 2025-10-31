using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Handlers;
using FluentAssertions;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class AddBookHandlerTests : InMemoryBookTestBase
    {
        private AddBookHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new AddBookHandler(Repo);
        }

        [Test]
        public async Task Should_Add_New_Book()
        {
            var command = new AddBookCommand("New Sample Book", "New Author");
            var result = await _handler.Handle(command, default);

            result.Should().NotBeNull();
            result.Title.Should().Be("New Sample Book");
            result.Author.Should().Be("New Author");

            var allBooks = await Repo.GetAllAsync();
            allBooks.Should().Contain(result);
        }
    }
}
