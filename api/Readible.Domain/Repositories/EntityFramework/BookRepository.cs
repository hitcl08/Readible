using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var bookViewModel = new BookViewModel
            {
                Name = book.Name,
                Author = book.Author,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                Rating = book.Rating,
            };
            var addedBook = _context.Books.Add(bookViewModel);
            await _context.SaveChangesAsync();

            return addedBook != null;
        }

        public async Task<bool> AddBookToSubscription(int subscriptionId, int bookId)
        {
            var subscriptionBook = new SubscriptionBookViewModel
            {
                SubscriptionId = subscriptionId,
                BookId = bookId
            };
            var addedSubBook = _context.SubscriptionBooks.Add(subscriptionBook);
            await _context.SaveChangesAsync();

            return addedSubBook != null;
        }

        public Book GetBook(int bookId)
        {
            var bookViewModel = _context.Books.Where(b => b.Id == bookId).FirstOrDefault();
            return _mapper.Map<BookViewModel, Book>(bookViewModel);
        }

        public async Task<List<Book>> GetBooks()
        {
            var bookViewModel = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookViewModel>, List<Book>>(bookViewModel);
        }

        public async Task<List<Book>> GetBooks(int subscriptionId)
        {
            var bookViewModel = _context.SubscriptionBooks.Join(
                _context.Books,
                sb => sb.BookId,
                b => b.Id,
                (b, sb) => new
                {
                    Id = sb.Id,
                    Author = sb.Author,
                    Description = sb.Description,
                    ImageUrl = sb.ImageUrl,
                    Rating = sb.Rating,
                    Name = sb.Name,
                    SubscriptionId = b.SubscriptionId
                })
                .Where(x => x.SubscriptionId == subscriptionId)
                .Select(x => new BookViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    Rating = x.Rating
                }).ToList();

            var bookList = _mapper.Map<List<BookViewModel>, List<Book>>(bookViewModel);
            return bookList;
        }

        public async Task<bool> RemoveBook(int bookId)
        {
            var bookToDelete = _context.Books.Where(b => b.Id == bookId).FirstOrDefault();

            var deletedBook = _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            return deletedBook != null;
        }

        public async Task<bool> RemoveBookFromSubscription(int subscriptionId, int bookId)
        {
            var subscriptionBookToDelete = _context.SubscriptionBooks
                .Where(sb => sb.BookId == bookId)
                .Where(sb => sb.SubscriptionId == subscriptionId)
                .FirstOrDefault();

            var deletedSubscriptionBook = _context.SubscriptionBooks.Remove(subscriptionBookToDelete);
            await _context.SaveChangesAsync();

            return deletedSubscriptionBook != null;
        }
    }
}
