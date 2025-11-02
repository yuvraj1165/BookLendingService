using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Handlers;
using BookLendingService.Data.DomainModel;
using Moq;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class AddBookHandlerTests : BookTestHandlerBase
    {
        private AddBookHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new AddBookHandler(mockRepo.Object);
        }

        [Test]
        public async Task Handle_ShoudInvokeAddAsyncWithCorrectBook()
        {
            var command = new AddBookCommand("A new book", "A New Author");

            await _handler.Handle(command, CancellationToken.None);

            mockRepo.Verify(r => r.AddAsync(It.Is<Book>(b => b.Title == "A new book" && b.Author == "A New Author")), Times.Once);
        }
    }
}
