using BookLendingService.Data.DomainModel;
using MediatR;

namespace BookLendingService.Application.Books.Commands
{
    public record AddBookCommand(string Title, string Author) : IRequest<Book>;

}
