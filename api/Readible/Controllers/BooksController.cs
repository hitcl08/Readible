using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readible.Domain.Interfaces;
using Readible.Domain.Models;
using Readible.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readible.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetByBookId([FromRoute] int id)
        {
            var book = _bookService.GetBook(id);
            return Ok(book);
        }

        [HttpGet("subscriptions/{id}")]
        public async Task<IActionResult> GetBySubscriptionId([FromRoute] int id)
        {
            var books = await _bookService.GetBooksBySubscription(id);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookRequest request)
        {
            var book = _mapper.Map<BookRequest, Book>(request);
            var isSuccessfull = await _bookService.AddBook(book);

            return isSuccessfull ? Created("book", isSuccessfull) : BadRequest(isSuccessfull);
        }

        [HttpPost("{bookId}/subscriptions/{subscriptionId}")]
        public async Task<IActionResult> AddBookToSubscription([FromRoute] int bookId, [FromRoute] int subscriptionId)
        {
            var isSuccessfull = await _bookService.AddBookToSubscription(subscriptionId, bookId);

            return isSuccessfull ? Ok(isSuccessfull) : BadRequest(isSuccessfull);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var isSuccessfull = await _bookService.RemoveBook(id);

            return isSuccessfull ? Ok(isSuccessfull) : BadRequest(isSuccessfull);
        }

        [HttpDelete("{bookId}/subscriptions/{subscriptionId}")]
        public async Task<IActionResult> DeleteBookFromSubscription([FromRoute] int bookId, [FromRoute] int subscriptionId)
        {
            var isSuccessfull = await _bookService.RemoveBookFromSubscription(subscriptionId, bookId);

            return isSuccessfull ? Ok(isSuccessfull) : BadRequest(isSuccessfull);
        }
    }
}
