using BookLendingService.Application.Books.Commands;
using BookLendingService.Data.DomainModel;
using BookLendingService.Data.Interfaces;
using MediatR;

namespace BookLendingService.Application.Books.Handlers
{
    public class AddBookHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IBookRepository _repo;

        public AddBookHandler(IBookRepository repo)
        {
            _repo = repo;
        }

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author
            };

            await _repo.AddAsync(book);
            return book;
        }
    }

}
