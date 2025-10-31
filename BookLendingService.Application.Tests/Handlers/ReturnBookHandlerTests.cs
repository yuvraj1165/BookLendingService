using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Handlers;
using FluentAssertions;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class ReturnBookHandlerTests : InMemoryBookTestBase
    {
        private ReturnBookHandler _handler = null!;

        [SetUp]
        public Task SetupHandler()
        {
            _handler = new ReturnBookHandler(Repo);
            return Task.CompletedTask;
        }

        [Test]
        public async Task Should_Return_CheckedOut_Book()
        {
            SampleBook.IsAvailable = false;
            var result = await _handler.Handle(new ReturnBookCommand(SampleBook.Id), default);
            var updated = await Repo.GetByIdAsync(SampleBook.Id);

            result.Should().BeTrue();
            updated!.IsAvailable.Should().BeTrue();
        }

        [Test]
        public async Task Should_Not_Return_Already_Available_Book()
        {
            SampleBook.IsAvailable = true;
            var result = await _handler.Handle(new ReturnBookCommand(SampleBook.Id), default);

            result.Should().BeFalse();
        }

        [Test]
        public async Task Should_Not_Return_Nonexistent_Book()
        {
            var result = await _handler.Handle(new ReturnBookCommand(Guid.NewGuid()), default);
            result.Should().BeFalse();
        }
    }
}
