using Moq;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Readible.Domain.Tests.Services
{
    public class BookServiceTests
    {
        private readonly MockRepository MockRepository;
        private readonly Mock<IBookRepository> _booksRepositoryMock;
        private readonly Mock<ISubscriptionRepository> _subscriptionRepositoryMock;

        private readonly IBookService _bookService;

        public BookServiceTests()
        {
            MockRepository = new MockRepository(MockBehavior.Loose);
            _booksRepositoryMock = MockRepository.Create<IBookRepository>();
            _subscriptionRepositoryMock = MockRepository.Create<ISubscriptionRepository>();
            _bookService = new BookService(_booksRepositoryMock.Object, _subscriptionRepositoryMock.Object);
        }

        [Fact]
        public async Task AddBookToSubscription_ReturnsTrue_WhenValidBookIdAndSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetInvalidBook());
            _booksRepositoryMock.Setup(x => x.AddBookToSubscription(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription);
            
            // act
            var isSuccessful = await _bookService.AddBookToSubscription(1,1);

            // assert
            Assert.True(isSuccessful);
        }

        [Fact]
        public async Task AddBookToSubscription_ReturnsFalse_WhenInvalidBookIdAndValidSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetInvalidBook());
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription);

            // act
            var isSuccessful = await _bookService.AddBookToSubscription(-1,1);

            // assert
            Assert.False(isSuccessful);
        }

        [Fact]
        public async Task AddBookToSubscription_ReturnsFalse_WhenValidBookIdAndInvalidSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook());
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetInvalidSubscription);

            // act
            var isSuccessful = await _bookService.AddBookToSubscription(1,-1);

            // assert
            Assert.False(isSuccessful);
        }


        [Fact]
        public async Task AddBookToSubscription_ReturnsFalse_WhenBookAlreadyAddedToSubscription()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook());
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription);

            // act
            var isSuccessful = await _bookService.AddBookToSubscription(1,1);

            // assert
            Assert.False(isSuccessful);
        }

        [Fact]
        public async Task AddBook_ReturnsTrue_WhenValidRequest()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetInvalidBook());
            _booksRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).ReturnsAsync(true);

            // act
            var isSuccessful = await _bookService.AddBook(new Book());

            // assert
            Assert.True(isSuccessful);
        }

        [Fact]
        public async Task AddBook_ReturnsFalse_WhenBookAlreadyExists()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook());

            // act
            var isSuccessful = await _bookService.AddBook(new Book());

            // assert
            Assert.False(isSuccessful);
        }

        [Fact]
        public void GetBook_ReturnsBook_WhenValidBookId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook());

            // act
            var book =  _bookService.GetBook(1);

            // assert
            Assert.NotNull(book);
        }

        [Fact]
        public void GetBook_ReturnsNull_WhenInvalidBookId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetInvalidBook());

            // act
            var book = _bookService.GetBook(-1);

            // assert
            Assert.Null(book);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsListOfBooks_WhenValidRequest()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBooks()).ReturnsAsync(MockData.Books.GetValidBooks());

            // act
            var books = await _bookService.GetAllBooks();

            // assert
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBooksBySubscription_ReturnsListOfBooks_WhenValidSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBooks(It.IsAny<int>())).ReturnsAsync(MockData.Books.GetValidBooks());
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription);

            // act
            var books = await _bookService.GetBooksBySubscription(1);

            // assert
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBooksBySubscription_ReturnsEmptyList_WhenInvalidSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBooks(It.IsAny<int>())).ReturnsAsync(new List<Book>());
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetInvalidSubscription);

            // act
            var books = await _bookService.GetBooksBySubscription(-1);

            // assert
            Assert.Empty(books);
        }

        [Fact]
        public async Task RemoveBook_ReturnsTrue_WhenValidBookId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook);
            _booksRepositoryMock.Setup(x => x.RemoveBook(It.IsAny<int>())).ReturnsAsync(true);

            // act
            var isSuccessful = await _bookService.RemoveBook(1);

            // assert
            Assert.True(isSuccessful);
        }

        [Fact]
        public async Task RemoveBook_ReturnsFalse_WhenInvalidBookId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetInvalidBook);
            _booksRepositoryMock.Setup(x => x.RemoveBook(It.IsAny<int>())).ReturnsAsync(false);

            // act
            var isSuccessful = await _bookService.RemoveBook(-1);

            // assert
            Assert.False(isSuccessful);
        }

        [Fact]
        public async Task RemoveBookFromSubscription_ReturnsTrue_WhenValidBookIdAndSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook);
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription);
            _booksRepositoryMock.Setup(x => x.RemoveBookFromSubscription(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            // act
            var isSuccessful = await _bookService.RemoveBookFromSubscription(1,1);

            // assert
            Assert.True(isSuccessful);
        }

        [Fact]
        public async Task RemoveBookFromSubscription_ReturnsFalse_WhenValidBookIdAndInvalidSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetValidBook);
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetInvalidSubscription);
            _booksRepositoryMock.Setup(x => x.RemoveBookFromSubscription(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            // act
            var isSuccessful = await _bookService.RemoveBookFromSubscription(1, -1);

            // assert
            Assert.False(isSuccessful);
        }

        [Fact]
        public async Task RemoveBookFromSubscription_ReturnsFalse_WhenInvalidBookIdAndValidSubscriptionId()
        {
            // arrange
            _booksRepositoryMock.Setup(x => x.GetBook(It.IsAny<int>())).Returns(MockData.Books.GetInvalidBook);
            _booksRepositoryMock.Setup(x => x.RemoveBookFromSubscription(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            _subscriptionRepositoryMock.Setup(x => x.GetSubscriptionBySubscriptionId(It.IsAny<int>())).Returns(MockData.Subscriptions.GetValidSubscription);

            // act
            var isSuccessful = await _bookService.RemoveBookFromSubscription(-1, 1);

            // assert
            Assert.False(isSuccessful);
        }
    }
}
