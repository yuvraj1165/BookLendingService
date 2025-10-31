using BookLendingService.Data.DomainModel;
using MediatR;

namespace BookLendingService.Application.Books.Queries
{
    public record GetAllBooksQuery : IRequest<List<Book>>;
}
