using Readible.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readible.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<bool> AddBook(Book book);
        Task<bool> AddBookToSubscription(int subscriptionId, int bookId);
        Book GetBook(int bookId);
        Task<List<Book>> GetBooks();
        Task<List<Book>> GetBooks(int subscriptionId);
        Task<bool> RemoveBook(int bookId);
        Task<bool> RemoveBookFromSubscription(int subscriptionId, int bookId);
    }
}
