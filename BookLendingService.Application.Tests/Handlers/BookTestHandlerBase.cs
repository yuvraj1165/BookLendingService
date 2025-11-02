using BookLendingService.Data.DomainModel;
using BookLendingService.Data.Interfaces;
using Moq;

namespace BookLendingService.Application.Tests.Handlers
{
    public abstract class BookTestHandlerBase
    {
        protected Mock<IBookRepository> mockRepo = null!;
        protected Book SampleBook = null!;

        [SetUp]
        public async Task BaseSetup()
        {
            mockRepo = new Mock<IBookRepository>();
            SampleBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Author = "Test Author",
                IsAvailable = true
            };

            var list = new List<Book> { SampleBook };

            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(list);
            mockRepo.Setup(x => x.GetByIdAsync(SampleBook.Id)).ReturnsAsync(SampleBook);
        }
    }
}
