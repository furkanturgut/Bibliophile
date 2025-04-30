using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos.AuthorDto;
using Backend.Interface.AuthorInterface;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Tüm yazarları getirir
        /// </summary>
        /// <returns>Yazarların listesi</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip yazarı getirir
        /// </summary>
        /// <returns>ID'si verilen yazar</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Yazar ID {id} bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Yeni bir yazar oluşturur
        /// </summary>
        /// <returns>Oluşturulan yazar</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdAuthor = await _authorService.AddAuthorAsync(authorDto);
                return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Var olan bir yazarı günceller
        /// </summary>
        /// <returns>Güncellenen yazar</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto authorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorDto);
                return Ok(updatedAuthor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Yazar ID {id} bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Bir yazarı siler
        /// </summary>
        /// <returns>Silinen yazar</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var deletedAuthor = await _authorService.DeleteAuthorAsync(id);
                return Ok(deletedAuthor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Yazar ID {id} bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Yazarın varlığını kontrol eder
        /// </summary>
        /// <returns>Yazarın var olup olmadığı bilgisi</returns>
        [HttpGet("exists/{id:int}")]
        public async Task<IActionResult> AuthorExists(int id)
        {
            try
            {
                var exists = await _authorService.AuthorExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}