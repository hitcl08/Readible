using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Models;
using Readible.Domain.Repositories.EntityFramework;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Readible.Domain.Tests.Repositories
{
    public class BookRepositoryTests
    {
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ReadibleContext>();
            dbContextOptionsBuilder.UseSqlServer("Server=tcp:readible.database.windows.net,1433;Initial Catalog=readible-db;Persist Security Info=False;User ID=liamhitchcock;Password=4fBuJ976YAUdwRXQ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .EnableSensitiveDataLogging();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<BookViewModel, Book>();
            });

            var mapper = config.CreateMapper();


            var context = new ReadibleContext(dbContextOptionsBuilder.Options);
            _bookRepository = new BookRepository(context, mapper);
        }

        [Fact]
        public async Task AddBook_AddsBookToLibrary_WhenValidBook()
        {
            // arrange
            var newBook = new Book
            {
                Name = "Dune 2"
            };

            // act
            var isSuccessful = await _bookRepository.AddBook(newBook);

            // assert
            Assert.True(isSuccessful);
        }

        [Fact]
        public async Task AddBookToSubscription_AddsBookToSubscription_WhenValidBook()
        {
            // arrange
            var subscriptionId = 1;
            var bookId = 5;

            // act
            var isSuccessful = await _bookRepository.AddBookToSubscription(subscriptionId, bookId);

            // assert
            Assert.True(isSuccessful);
        }

        [Fact]
        public void GetBook_ReturnsBook_WhenValidBookId()
        {
            // arrange
            var bookId = 3;

            // act
            var book = _bookRepository.GetBook(bookId);

            // assert
            Assert.NotNull(book);
        }

        [Fact]
        public void GetBook_ReturnsNull_WhenInvalidBookId()
        {
            // arrange
            var bookId = -1;

            // act
            var book = _bookRepository.GetBook(bookId);

            // assert
            Assert.Null(book);
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfAllBooks_WhenEmptyParams()
        {
            // act
            var books = await _bookRepository.GetBooks();

            // assert
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBooks_ReturnsListOfSubscriptionBooks_WhenValidSubscriptionId()
        {
            // arrange
            var subId = 1;

            // act
            var subscriptionBooks = await _bookRepository.GetBooks(subId);

            // assert
            Assert.NotEmpty(subscriptionBooks);
        }

        [Fact]
        public async Task RemoveBook_ReturnsTrue_WhenValidBookId()
        {
            // arrange
            var bookId = 5;

            // act
            var isDeleted = await _bookRepository.RemoveBook(bookId);

            // assert
            Assert.True(isDeleted);
        }

        [Fact]
        public async Task RemoveBook_ReturnsTrue_WhenValidBookIdAndSubscriptionId()
        {
            // arrange
            var subId = 1;
            var bookId = 5;

            // act
            var isDeleted = await _bookRepository.RemoveBookFromSubscription(subId, bookId);

            // assert
            Assert.True(isDeleted);
        }
    }
}
