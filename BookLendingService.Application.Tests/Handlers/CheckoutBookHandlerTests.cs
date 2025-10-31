using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Handlers;
using FluentAssertions;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class CheckoutBookHandlerTests : InMemoryBookTestBase
    {
        private CheckoutBookHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new CheckoutBookHandler(Repo);
        }

        [Test]
        public async Task Should_CheckOut_Available_Book()
        {
            SampleBook.IsAvailable = true;
            var result = await _handler.Handle(new CheckoutBookCommand(SampleBook.Id), default);

            result.Should().BeTrue();
            SampleBook.IsAvailable.Should().BeFalse();
        }

        [Test]
        public async Task Should_Not_CheckOut_Already_CheckedOut_Book()
        {
            SampleBook.IsAvailable = false;
            var result = await _handler.Handle(new CheckoutBookCommand(SampleBook.Id), default);

            result.Should().BeFalse();
            SampleBook.IsAvailable.Should().BeFalse();
        }

        [Test]
        public async Task Should_Not_CheckOut_Nonexistent_Book()
        {
            var result = await _handler.Handle(new CheckoutBookCommand(Guid.NewGuid()), default);
            result.Should().BeFalse();
        }
    }
}