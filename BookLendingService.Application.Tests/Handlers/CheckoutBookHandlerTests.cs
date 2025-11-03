using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Handlers;
using BookLendingService.Data.DomainModel;
using Moq;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class CheckoutBookHandlerTests : BookTestHandlerBase
    {
        private CheckoutBookHandler handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            handler = new CheckoutBookHandler(mockRepo.Object);
        }

        [Test]
        public async Task Handle_ShouldInvokeCheckoutAsync()
        {
            var command = new CheckoutBookCommand(SampleBook.Id);

            await handler.Handle(command, CancellationToken.None);

            mockRepo.Verify(r => r.CheckoutAsync(It.IsAny<Book>()), Times.Once);
        }
    }
}