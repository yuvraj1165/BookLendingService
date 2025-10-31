using BookLendingService.Data.DomainModel;
using MediatR;

namespace BookLendingService.Application.Books.Queries
{
    public record GetBookByIdQuery(Guid Id) : IRequest<Book?>;
}
