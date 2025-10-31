using BookLendingService.Data.DomainModel;
using BookLendingService.Data.Interfaces;

namespace BookLendingService.Data.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();

        public Task<List<Book>> GetAllAsync() => Task.FromResult(_books.ToList());

        public Task<Book?> GetByIdAsync(Guid id) =>
            Task.FromResult(_books.FirstOrDefault(b => b.Id == id));

        public Task AddAsync(Book book)
        {
            book.Id = Guid.NewGuid();
            _books.Add(book);
            return Task.CompletedTask;
        }

        public Task<bool> CheckoutAsync(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null || book.IsAvailable) return Task.FromResult(false);
            book.IsAvailable = true;
            return Task.FromResult(true);
        }

        public Task<bool> ReturnAsync(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null || !book.IsAvailable) return Task.FromResult(false);
            book.IsAvailable = false;
            return Task.FromResult(true);
        }
    }
}
