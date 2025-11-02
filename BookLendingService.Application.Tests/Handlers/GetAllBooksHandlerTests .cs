using BookLendingService.Application.Books.Handlers;
using BookLendingService.Application.Books.Queries;
using FluentAssertions;
using Moq;

namespace BookLendingService.Application.Tests.Handlers
{
    [TestFixture]
    public class GetAllBooksHandlerTests : BookTestHandlerBase
    {
        private GetAllBooksHandler _handler = null!;

        [SetUp]
        public void SetupHandler()
        {
            _handler = new GetAllBooksHandler(mockRepo.Object);
        }

        [Test]
        public async Task Handle_ShoudInvokeGetAllAsync()
        {
            var query = new GetAllBooksQuery();

            await _handler.Handle(query, CancellationToken.None);

            mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Return_AllBooks()
        {
            var result = await _handler.Handle(new GetAllBooksQuery(), default);

            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.First().Title.Should().Be(SampleBook.Title);
        }
    }
}