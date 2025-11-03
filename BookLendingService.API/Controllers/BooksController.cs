using BookLendingService.Application.Books.Commands;
using BookLendingService.Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookLendingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            return book is not null ? Ok(book) : NotFound($"Book with ID {id} not found.");
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
        {
            var book = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPost("{id}/checkout")]
        public async Task<IActionResult> CheckoutBook(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new CheckoutBookCommand(id));

                if (!result)
                    return NotFound($"Book {id} not available.");

                return Ok($"Book {id} checked out.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnBook(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new ReturnBookCommand(id));

                if (!result)
                    return NotFound($"Book {id} not found or not checked out.");

                return Ok($"Book {id} returned.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
