using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos.BlogPostDtos;
using Backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        /// <summary>
        /// Tüm blog yazılarını getirir
        /// </summary>
        /// <returns>Blog yazılarının listesi</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            try
            {
                var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
                return Ok(blogPosts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Belirtilen ID'ye sahip blog yazısını getirir
        /// </summary>
        /// <returns>ID'si verilen blog yazısı</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            try
            {
                var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
                if (blogPost == null)
                    return NotFound($"Blog yazısı ID {id} bulunamadı");
                
                return Ok(blogPost);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Belirtilen kullanıcının blog yazılarını getirir
        /// </summary>
        /// <returns>Kullanıcının blog yazılarının listesi</returns>
        [HttpGet("author/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBlogPostsByAuthor(string userId)
        {
            try
            {
                var blogPosts = await _blogPostService.GetBlogPostsByAuthorIdAsync(userId);
                return Ok(blogPosts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Son eklenen belirli sayıda blog yazısını getirir
        /// </summary>
        /// <returns>Son eklenen blog yazılarının listesi</returns>
        [HttpGet("recent/{count:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecentBlogPosts(int count)
        {
            try
            {
                var blogPosts = await _blogPostService.GetRecentBlogPostsAsync(count);
                return Ok(blogPosts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Yeni bir blog yazısı oluşturur
        /// </summary>
        /// <returns>Oluşturulan blog yazısı</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostDto blogPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Giriş yapmış kullanıcının ID'sini al
                var userId = "02017753-34b3-4c58-a873-98b46937fef2";
                
                var createdBlogPost = await _blogPostService.AddBlogPostAsync(blogPostDto, userId);
                return CreatedAtAction(nameof(GetBlogPostById), new { id = createdBlogPost.Id }, createdBlogPost);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500,"Internal Server Error" );
            }
        }

        /// <summary>
        /// Var olan bir blog yazısını günceller
        /// </summary>
        /// <returns>Güncellenen blog yazısı</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] UpdateBlogPostDto blogPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Yazı sahibi veya admin olduğunu kontrol etmek için burada bir yetkilendirme kontrolü yapılabilir
                
                var updatedBlogPost = await _blogPostService.UpdateBlogPostAsync(id, blogPostDto);
                if (updatedBlogPost == null)
                    return NotFound($"Blog yazısı ID {id} bulunamadı");
                
                return Ok(updatedBlogPost);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Bir blog yazısını siler
        /// </summary>
        /// <returns>Silinen blog yazısı</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            try
            {
                // Yazı sahibi veya admin olduğunu kontrol etmek için burada bir yetkilendirme kontrolü yapılabilir

                var deletedBlogPost = await _blogPostService.DeleteBlogPostAsync(id);
                if (deletedBlogPost == null)
                    return NotFound($"Blog yazısı ID {id} bulunamadı");
                
                return Ok(deletedBlogPost);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Blog yazısının var olup olmadığını kontrol eder
        /// </summary>
        /// <returns>Blog yazısının var olup olmadığı bilgisi</returns>
        [HttpGet("exists/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BlogPostExists(int id)
        {
            try
            {
                var exists = await _blogPostService.BlogPostExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}