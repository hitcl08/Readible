using AutoMapper;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Repositories.EntityFramework
{
    public class BookRepository : IBookRepository
    {
        private readonly IMapper _mapper;
        protected readonly ReadibleContext _context;

        public BookRepository(ReadibleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddBook(Book book)
        {
            var addedBook = _context.Book
                .Add(new BookViewModel
                {
                    SubscriptionId = null
                });

            var addedBookDetails = _context.BookDetails
                .Add(new BookDetailsViewModel
                {
                    BookId = book.Id,
                    Name = book.Name,
                    ImageUrl = book.ImageUrl,
                    Description = book.Description,
                    Rating = book.Rating
                });

            await _context.SaveChangesAsync();

            return addedBook != null && addedBookDetails != null;
        }

        public Task<bool> AddBookToSubscription(int subscriptionId, int bookId)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooks()
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooks(int subscriptionId)
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
