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
                Name = "Gone wif da wind",
                Id = 1,
                Subscriptions = new List<Subscription>()
            };

            // act
            var isSuccessful = await _bookRepository.AddBook(newBook);

            // assert
            Assert.True(isSuccessful);
        }
    }
}
