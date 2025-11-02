using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Handlers;
using BookLendingService.Data.DomainModel;
using Moq;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class ReturnBookHandlerTests : BookTestHandlerBase
    {
        private ReturnBookHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            SampleBook.IsAvailable = false;
            _handler = new ReturnBookHandler(mockRepo.Object);
        }

        [Test]
        public async Task Handle_ShoudInvokeReturnAsync()
        {
            var command = new ReturnBookCommand(SampleBook.Id);

            await _handler.Handle(command, CancellationToken.None);

            mockRepo.Verify(r => r.ReturnAsync(It.IsAny<Book>()), Times.Once);
        }
    }
}
