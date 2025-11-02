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

        public Task CheckoutAsync(Book book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            if (!book.IsAvailable)
                throw new InvalidOperationException("Book is already checked out.");

            book.IsAvailable = false;
            return Task.CompletedTask;
        }

        public Task ReturnAsync(Book book)
        {
            if (book is null)
                throw new ArgumentNullException(nameof(book));

            if (book.IsAvailable)
                throw new InvalidOperationException("Book cannot be returned as not checked out");

            book.IsAvailable = true;
            return Task.CompletedTask;
        }
    }
}
