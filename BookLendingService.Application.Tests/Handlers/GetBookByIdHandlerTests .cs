using BookLendingService.Application.Books.Handlers;
using BookLendingService.Application.Books.Queries;
using Moq;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class GetBookByIdHandlerTests : BookTestHandlerBase
    {
        private GetBookByIdHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new GetBookByIdHandler(mockRepo.Object);
        }

        [Test]
        public async Task Handle_Should_InvokeGetByIdAsync()
        {
            var query = new GetBookByIdQuery(Guid.Empty);

            await _handler.Handle(query, CancellationToken.None);

            mockRepo.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
