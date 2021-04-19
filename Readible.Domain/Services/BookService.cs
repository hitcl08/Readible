using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Services
{
    public class BookService : IBookService
    {
        public Task<bool> AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddBookToSubscription(int subscriptionId, int bookId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooksBySubscription(int subscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBookFromSubscription(int subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
