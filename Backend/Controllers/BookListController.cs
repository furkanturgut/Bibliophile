using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Dtos.BookDtos;
using Backend.Dtos.BookListDtos;
using Backend.Extensions;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookListController : ControllerBase
    {
        private readonly IBookListService _bookListService;
  
        public BookListController(IBookListService bookListService)
        {
            this._bookListService = bookListService;

        }


        [HttpGet]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBookLists()
        {
            try
            {
                var bookLists = await _bookListService.GetAllBookListsAsync();
                return Ok(bookLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpGet("{bookId:int}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookListById(int bookId)
        {
            try
            {
                var bookList = await _bookListService.GetBookListByIdAsync(bookId);
                return Ok(bookList);
            }
            catch (KeyNotFoundException)
            {
                 return NotFound($"ID {bookId} ile kitap listesi bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookListsByUserId(string userId)
        {
            try
            {
                var bookLists = await _bookListService.GetBookListsByUserIdAsync(userId);
                return Ok(bookLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("my-lists")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> GetMyBookLists()
        {
            try
            {
                var userName = User.GetUserName();
                var bookLists = await _bookListService.GetBookListsByUserIdAsync(userName);
                return Ok(bookLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("popular/{count:int}")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPopularBookLists(int count)
        {
            try
            {
                var bookLists = await _bookListService.GetPopularBookListsAsync(count);
                return Ok(bookLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<IActionResult> CreateBookList([FromBody] CreateBookListDto createBookListDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userName = User.GetUserName();
                var createdBookList = await _bookListService.CreateBookListAsync(createBookListDto, userName);

                return CreatedAtAction(nameof(GetBookListById), new { id = createdBookList.Id }, createdBookList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBookList(int id, [FromBody] UpdateBookListDto updateBookListDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var userName = User.GetUserName();
                var isOwner = await _bookListService.IsUserListOwnerAsync(id, userName);

                if (!isOwner && !User.IsInRole("Admin"))
                    return Forbid("Bu listeyi düzenleme yetkiniz yok");

                var updatedBookList = await _bookListService.UpdateBookListAsync(id, updateBookListDto);
                if (updatedBookList == null)
                    return NotFound($"ID {id} ile kitap listesi bulunamadı");

                return Ok(updatedBookList);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listesi güncellenirken bir hata oluştu");
            }
        }


        [HttpDelete("{bookId:int}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        public async Task<IActionResult> DeleteBookList(int bookId)
        {
            try
            {

                var userName = User.GetUserName();
                var isOwner = await _bookListService.IsUserListOwnerAsync(bookId,userName);

                if (!isOwner && !User.IsInRole("Admin"))
                    return Forbid("Bu listeyi silme yetkiniz yok");

                var deletedBookList = await _bookListService.DeleteBookListAsync(bookId);


                return Ok(deletedBookList);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"ID {bookId} ile kitap listesi bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("{id:int}/books")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBookToList(int id, [FromBody] AddBookToListDto addBookToListDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Liste sahibi veya admin olup olmadığını kontrol et
                var userName = User.GetUserName();
                var isOwner = await _bookListService.IsUserListOwnerAsync(id, userName);

                if (!isOwner && !User.IsInRole("Admin"))
                    return Forbid("Bu listeye kitap ekleme yetkiniz yok");

                var updatedBookList = await _bookListService.AddBookToListAsync(id, addBookToListDto);
                return Ok(updatedBookList);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{listId:int}/books/{bookId:int}")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveBookFromList(int listId, int bookId)
        {
            try
            {
                // Liste sahibi veya admin olup olmadığını kontrol et
                var userName = User.GetUserName();
                var isOwner = await _bookListService.IsUserListOwnerAsync(listId, userName);

                if (!isOwner && !User.IsInRole("Admin"))
                    return Forbid("Bu listeden kitap silme yetkiniz yok");

                var updatedBookList = await _bookListService.RemoveBookFromListAsync(listId, bookId);
                return Ok(updatedBookList);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"ID {listId} ile kitap listesi bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}