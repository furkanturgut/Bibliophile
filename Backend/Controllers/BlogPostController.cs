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

       
        [HttpGet]
        [ProducesResponseType(typeof(List<BlogPostDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            try
            {
                var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

   
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BlogPostDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            try
            {
                var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
                return Ok(blogPost);
            }
            catch(KeyNotFoundException)
            {
                return NotFound($"Blog yazısı ID {id} bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("author/{authorId:int}")]
        [ProducesResponseType(typeof(List<BlogPostDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBlogPostsByAuthor(int authorId)
        {
            try
            {
                var blogPosts = await _blogPostService.GetBlogPostsByAuthorIdAsync(authorId);
                return Ok(blogPosts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(BlogPostDto),StatusCodes.Status201Created)]
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
                
                return StatusCode(500,ex.Message);
            }
        }

     
        [HttpPut("{userId:int}")]
        [ProducesResponseType(typeof(BlogPostDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBlogPost(int userId, [FromBody] UpdateBlogPostDto blogPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Yazı sahibi veya admin olduğunu kontrol etmek için burada bir yetkilendirme kontrolü yapılabilir. Claims Service ile userId kismi silinecek. 
                
                var updatedBlogPost = await _blogPostService.UpdateBlogPostAsync(userId, blogPostDto);

                return Ok(updatedBlogPost);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Blog yazısı ID {userId} bulunamadı");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{postId:int}")]
        [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBlogPost(int postId)
        {
            try
            {
                // Yazı sahibi veya admin olduğunu kontrol etmek için burada bir yetkilendirme kontrolü yapılabilir

                var deletedBlogPost = await _blogPostService.DeleteBlogPostAsync(postId);
                
                
                return Ok(deletedBlogPost);
            }
            catch(KeyNotFoundException)
            {
                return NotFound($"Blog yazısı ID {postId} bulunamadı");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    
        [HttpGet("exists/{postId:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BlogPostExists(int postId)
        {
            try
            {
                var exists = await _blogPostService.BlogPostExistsAsync(postId);
                return Ok(exists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}