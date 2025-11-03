using BookLendingService.API.Controllers;
using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Queries;
using BookLendingService.Data.DomainModel;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookLendingService.API.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private BooksController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BooksController(_mediatorMock.Object);
        }


        [Test]
        public async Task Get_ReturnsOk_WithListOfBooks()
        {
            var books = new List<Book> { new Book { Id = Guid.NewGuid(), Title = "Test Book" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBooksQuery>(), default)).ReturnsAsync(books);

            var result = await _controller.Get();

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult!.Value.Should().BeEquivalentTo(books);
        }

        [Test]
        public async Task GetBookById_ShouldReturnOk_WhenBookExists()
        {
            var bookId = Guid.NewGuid();
            var expectedBook = new Book { Id = bookId, Title = "Existing Book" };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetBookByIdQuery>(q => q.Id == bookId), default))
                .ReturnsAsync(expectedBook);

            var result = await _controller.GetBookById(bookId);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(expectedBook);
        }

        [Test]
        public async Task GetBookById_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            var bookId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetBookByIdQuery>(q => q.Id == bookId), default))
                .ReturnsAsync((Book)null);

            var result = await _controller.GetBookById(bookId);

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFound = result as NotFoundObjectResult;
            notFound!.Value.Should().Be($"Book with ID {bookId} not found.");
        }


        [Test]
        public async Task AddBook_ShouldReturnCreatedAtAction_WithBook()
        {
            var command = new AddBookCommand("New Book", "New Author");
            var createdBook = new Book { Id = Guid.NewGuid(), Title = command.Title };

            _mediatorMock
                .Setup(m => m.Send(command, default))
                .ReturnsAsync(createdBook);

            var result = await _controller.AddBook(command);

            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = result as CreatedAtActionResult;
            createdResult!.Value.Should().BeEquivalentTo(createdBook);
            createdResult.ActionName.Should().Be(nameof(_controller.GetBookById));
        }

        [Test]
        public async Task CheckoutBook_ShouldReturnOk_WhenSuccessful()
        {
            var bookId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.Is<CheckoutBookCommand>(c => c.Id == bookId), default))
                .ReturnsAsync(true);

            var result = await _controller.CheckoutBook(bookId);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be($"Book {bookId} checked out.");
        }

        [Test]
        public async Task CheckoutBook_ShouldReturnNotFound_WhenBookUnavailable()
        {
            var bookId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CheckoutBookCommand>(), default))
                .ReturnsAsync(false);

            var result = await _controller.CheckoutBook(bookId);

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFound = result as NotFoundObjectResult;
            notFound!.Value.Should().Be($"Book {bookId} not available.");
        }

        [Test]
        public async Task ReturnBook_ShouldReturnOk_WhenSuccessful()
        {
            var bookId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.Is<ReturnBookCommand>(c => c.Id == bookId), default))
                .ReturnsAsync(true);

            var result = await _controller.ReturnBook(bookId);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().Be($"Book {bookId} returned.");
        }

        [Test]
        public async Task ReturnBook_ShouldReturnNotFound_WhenBookNotCheckedOut()
        {
            var bookId = Guid.NewGuid();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ReturnBookCommand>(), default))
                .ReturnsAsync(false);

            var result = await _controller.ReturnBook(bookId);

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFound = result as NotFoundObjectResult;
            notFound!.Value.Should().Be($"Book {bookId} not found or not checked out.");
        }
    }
}
