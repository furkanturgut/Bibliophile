using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos;
using Backend.Dtos.BookDtos;
using Backend.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>List of all books</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get a book by ID
        /// </summary>
        /// <returns>Book with specified ID</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <returns>Created book</returns>
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdBook = await _bookService.AddBookAsync(bookDto);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        /// <returns>Updated book</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto bookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedBook = await _bookService.UpdateBookAsync(bookDto, id);
                return Ok(updatedBook);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <returns>Deleted book</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var deletedBook = await _bookService.DeleteBookAsync(id);
                return Ok(deletedBook);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {id} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get books by author
        /// </summary>
        /// <returns>List of books by specified author</returns>
        [HttpGet("byAuthor/{authorId:int}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            try
            {
                var books = await _bookService.GetBooksByAuthorAsync(authorId);
                return Ok(books);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Author with ID {authorId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get books by genre
        /// </summary>
        /// <returns>List of books in specified genre</returns>
        [HttpGet("byGenre/{genreId:int}")]
        public async Task<IActionResult> GetBooksByGenre(int genreId)
        {
            try
            {
                var books = await _bookService.GetBooksByGenreAsync(genreId);
                return Ok(books);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Genre with ID {genreId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
