using BookLendingService.Application.Books.Queries;
using BookLendingService.Data.DomainModel;
using BookLendingService.Data.Interfaces;
using MediatR;

namespace BookLendingService.Application.Books.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book?>
    {
        private readonly IBookRepository _repo;

        public GetBookByIdHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }

}
