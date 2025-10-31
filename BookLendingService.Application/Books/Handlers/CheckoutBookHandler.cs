using BookLendingService.Application.Books.Commands;
using BookLendingService.Data.Interfaces;
using MediatR;

namespace BookLendingService.Application.Books.Handlers
{
    public class CheckoutBookHandler : IRequestHandler<CheckoutBookCommand, bool>
    {
        private readonly IBookRepository _repo;

        public CheckoutBookHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(CheckoutBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repo.GetByIdAsync(request.Id);
            if (book is null || !book.IsAvailable) return false;

            book.IsAvailable = false;
            return true;
        }
    }
}
