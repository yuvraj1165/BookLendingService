using MediatR;

namespace BookLendingService.Application.Books.Commands
{
    public record ReturnBookCommand(Guid Id) : IRequest<bool>;
}
