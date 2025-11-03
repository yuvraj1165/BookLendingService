using BookLendingService.Data.DomainModel;
using BookLendingService.Data.Repositories;
using FluentAssertions;

namespace BookLendingService.Data.Tests
{
    [TestFixture]
    public class InMemoryBookRepositoryTests
    {
        private InMemoryBookRepository _repo = null!;
        private Book _book = null!;

        [SetUp]
        public void Setup()
        {
            _repo = new InMemoryBookRepository();
            _book = new Book
            {
                Title = "New Sample Book",
                Author = "New Author",
                IsAvailable = true
            };
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllBooks()
        {
            var anotherBook = new Book { Title = "Another Book", Author = "Another Author" };
            await _repo.AddAsync(_book);
            await _repo.AddAsync(anotherBook);

            var allBooks = await _repo.GetAllAsync();
            allBooks.Should().HaveCount(2);
            allBooks.Should().Contain(b => b.Title == "New Sample Book");
            allBooks.Should().Contain(b => b.Title == "Another Book");
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectBook()
        {
            await _repo.AddAsync(_book);

            var result = await _repo.GetByIdAsync(_book.Id);
            result.Should().NotBeNull();
            result!.Title.Should().Be("New Sample Book");
        }

        [Test]
        public async Task AddAsync_ShouldStoreBook()
        {
            await _repo.AddAsync(_book);

            var allBooks = await _repo.GetAllAsync();
            allBooks.Should().ContainSingle(b => b.Title == "New Sample Book" && b.Author == "New Author");
        }

        [Test]
        public async Task CheckoutAsync_ShouldCheckoutBookAndSetAsUnavailable()
        {
            _book.IsAvailable = true;
            await _repo.AddAsync(_book);
            await _repo.CheckoutAsync(_book);

            var checkedOutBook = await _repo.GetByIdAsync(_book.Id);
            checkedOutBook!.IsAvailable.Should().BeFalse();
        }

        [Test]
        public async Task CheckoutAsync_ShouldNotCheckOutAlreadyCheckedOutBook()
        {
            _book.IsAvailable = false;
            await _repo.AddAsync(_book);

            Func<Task> act = () => _repo.CheckoutAsync(_book);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Book is already checked out.");
        }

        [Test]
        public async Task ReturnAsync_ShouldMarkBookAsAvailable()
        {
            _book.IsAvailable = false;
            await _repo.AddAsync(_book);
            await _repo.ReturnAsync(_book);

            var returnedBook = await _repo.GetByIdAsync(_book.Id);
            returnedBook!.IsAvailable.Should().BeTrue();
        }
    }
}