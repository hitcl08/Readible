using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface IBookService
    {
        Task<bool> AddBook(Book book);
        Task<bool> AddBookToSubscription(int subscriptionId, int bookId);
        Book GetBook(int bookId);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBooksBySubscription(int subscriptionId);
        Task<bool> RemoveBook(int bookId);
        Task<bool> RemoveBookFromSubscription(int subscriptionId, int bookId);
    }
}
