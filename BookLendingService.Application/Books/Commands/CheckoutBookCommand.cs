using MediatR;

namespace BookLendingService.Application.Books.Commands
{
    public record CheckoutBookCommand(Guid Id) : IRequest<bool>;
}
