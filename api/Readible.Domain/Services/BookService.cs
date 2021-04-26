using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public BookService(IBookRepository bookRepository, ISubscriptionRepository subscriptionRepository)
        {
            _bookRepository = bookRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<bool> AddBook(Book book)
        {
            var existingBook = _bookRepository.GetBook(book.Id);
            if (existingBook != null)
            {
                return false;
            }

            return await _bookRepository.AddBook(book);
        }

        public async Task<bool> AddBookToSubscription(int subscriptionId, int bookId)
        {
            var book = _bookRepository.GetBook(bookId);
            var subscription = _subscriptionRepository.GetSubscriptionBySubscriptionId(subscriptionId);

            // if book already exists in subscription AND subscription does not exist
            if (book != null && subscription == null)
            {
                return false;
            }

            return await _bookRepository.AddBookToSubscription(subscriptionId, bookId);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepository.GetBooks();
        }

        public Book GetBook(int bookId)
        {
            return _bookRepository.GetBook(bookId);
        }

        public async Task<List<Book>> GetBooksBySubscription(int subscriptionId)
        {
            var subscription = _subscriptionRepository.GetSubscriptionBySubscriptionId(subscriptionId);
            if (subscription == null)
            {
                return new List<Book>();
            }
            return await _bookRepository.GetBooks(subscriptionId);
        }

        public async Task<bool> RemoveBook(int bookId)
        {
            var book = _bookRepository.GetBook(bookId);
            if (book == null)
            {
                return false;
            }

            return await _bookRepository.RemoveBook(bookId);
        }

        public async Task<bool> RemoveBookFromSubscription(int subscriptionId, int bookId)
        {
            var subscription = _subscriptionRepository.GetSubscriptionBySubscriptionId(subscriptionId);
            var book = _bookRepository.GetBook(bookId);

            if (subscription == null || book == null)
            {
                return false;
            }

            return await _bookRepository.RemoveBookFromSubscription(subscriptionId, bookId);
        }
    }
}
