using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Dtos.BookListDtos;
using Backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            _bookListService = bookListService;
        }

        /// <summary>
        /// Tüm kitap listelerini getirir
        /// </summary>
        /// <returns>Kitap listelerinin listesi</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBookLists()
        {
            try
            {
                var bookLists = await _bookListService.GetAllBookListsAsync();
                return Ok(bookLists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listeleri getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// ID'ye göre kitap listesi getirir
        /// </summary>
        /// <returns>ID'ye göre kitap listesi</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookListById(int id)
        {
            try
            {
                var bookList = await _bookListService.GetBookListByIdAsync(id);
                if (bookList == null)
                    return NotFound($"ID {id} ile kitap listesi bulunamadı");
                
                return Ok(bookList);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listesi getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Kullanıcıya göre kitap listelerini getirir
        /// </summary>
        /// <returns>Kullanıcıya ait kitap listeleri</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookListsByUser(string userId)
        {
            try
            {
                var bookLists = await _bookListService.GetBookListsByUserIdAsync(userId);
                return Ok(bookLists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listeleri getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Giriş yapmış kullanıcıya ait kitap listelerini getirir
        /// </summary>
        /// <returns>Kullanıcıya ait kitap listeleri</returns>
        [HttpGet("my-lists")]
        [Authorize]
        public async Task<IActionResult> GetMyBookLists()
        {
            try
            {
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var bookLists = await _bookListService.GetBookListsByUserIdAsync(userId);
                return Ok(bookLists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listeleri getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Popüler kitap listelerini getirir
        /// </summary>
        /// <returns>Popüler kitap listeleri</returns>
        [HttpGet("popular/{count:int}")]
        public async Task<IActionResult> GetPopularBookLists(int count)
        {
            try
            {
                var bookLists = await _bookListService.GetPopularBookListsAsync(count);
                return Ok(bookLists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Popüler listeler getirilirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Yeni bir kitap listesi oluşturur
        /// </summary>
        /// <returns>Oluşturulan kitap listesi</returns>
        [HttpPost]

        public async Task<IActionResult> CreateBookList([FromBody] CreateBookListDto createBookListDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var createdBookList = await _bookListService.CreateBookListAsync(createBookListDto, userId);
                
                return CreatedAtAction(nameof(GetBookListById), new { id = createdBookList.Id }, createdBookList);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listesi oluşturulurken bir hata oluştu");
            }
        }

        /// <summary>
        /// Var olan bir kitap listesini günceller
        /// </summary>
        /// <returns>Güncellenen kitap listesi</returns>
        [HttpPut("{id:int}")]

        public async Task<IActionResult> UpdateBookList(int id, [FromBody] UpdateBookListDto updateBookListDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

              
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isOwner = await _bookListService.IsUserListOwnerAsync(id, userId);
                
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

        /// <summary>
        /// Bir kitap listesini siler
        /// </summary>
        /// <returns>Silinen kitap listesi</returns>
        [HttpDelete("{id:int}")]

        public async Task<IActionResult> DeleteBookList(int id)
        {
            try
            {
               
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isOwner = await _bookListService.IsUserListOwnerAsync(id, userId);
                
                if (!isOwner && !User.IsInRole("Admin"))
                    return Forbid("Bu listeyi silme yetkiniz yok");

                var deletedBookList = await _bookListService.DeleteBookListAsync(id);
                if (deletedBookList == null)
                    return NotFound($"ID {id} ile kitap listesi bulunamadı");
                
                return Ok(deletedBookList);
            }
            catch (Exception)
            {
                return StatusCode(500, "Kitap listesi silinirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Listeye kitap ekler
        /// </summary>
        /// <returns>Güncellenmiş kitap listesi</returns>
        [HttpPost("{id:int}/books")]

        public async Task<IActionResult> AddBookToList(int id, [FromBody] AddBookToListDto addBookToListDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Liste sahibi veya admin olup olmadığını kontrol et
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isOwner = await _bookListService.IsUserListOwnerAsync(id, userId);
                
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
            catch (Exception)
            {
                return StatusCode(500, "Listeye kitap eklenirken bir hata oluştu");
            }
        }

        /// <summary>
        /// Listeden kitap çıkarır
        /// </summary>
        /// <returns>Güncellenmiş kitap listesi</returns>
        [HttpDelete("{id:int}/books/{bookId:int}")]

        public async Task<IActionResult> RemoveBookFromList(int id, int bookId)
        {
            try
            {
                // Liste sahibi veya admin olup olmadığını kontrol et
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                var isOwner = await _bookListService.IsUserListOwnerAsync(id, userId);
                
                if (!isOwner && !User.IsInRole("Admin"))
                    return Forbid("Bu listeden kitap silme yetkiniz yok");

                var updatedBookList = await _bookListService.RemoveBookFromListAsync(id, bookId);
                if (updatedBookList == null)
                    return NotFound($"ID {id} ile kitap listesi bulunamadı");
                
                return Ok(updatedBookList);
            }
            catch (Exception)
            {
                return StatusCode(500, "Listeden kitap silinirken bir hata oluştu");
            }
        }
    }
}