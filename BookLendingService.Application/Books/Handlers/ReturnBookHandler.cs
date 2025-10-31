using BookLendingService.Application.Books.Commands;
using BookLendingService.Data.Interfaces;
using MediatR;

namespace BookLendingService.Application.Books.Handlers
{
    public class ReturnBookHandler : IRequestHandler<ReturnBookCommand, bool>
    {
        private readonly IBookRepository _repo;

        public ReturnBookHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repo.GetByIdAsync(request.Id);
            if (book is null || book.IsAvailable) return false;

            book.IsAvailable = true;
            return true;
        }
    }
}
