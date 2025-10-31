using BookLendingService.Data.DomainModel;
using BookLendingService.Data.Repositories;

namespace BookLendingService.Application.Tests.Handlers
{
    public abstract class InMemoryBookTestBase
    {
        protected InMemoryBookRepository Repo = null!;
        protected Book SampleBook = null!;

        [SetUp]
        public async Task BaseSetup()
        {
            Repo = new InMemoryBookRepository();
            SampleBook = new Book
            {
                Title = "Test Book",
                Author = "Test Author",
                IsAvailable = true
            };
            await Repo.AddAsync(SampleBook);
        }
    }
}
