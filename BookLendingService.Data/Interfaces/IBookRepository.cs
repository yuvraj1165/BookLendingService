using BookLendingService.Data.DomainModel;

namespace BookLendingService.Data.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task CheckoutAsync(Book book);
        Task ReturnAsync(Book book);
    }
}