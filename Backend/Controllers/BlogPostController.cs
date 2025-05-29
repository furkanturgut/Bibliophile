using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Dtos.BlogPostDtos;
using Backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Backend.Extensions;

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
        [ProducesResponseType(typeof(List<BlogPostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            try
            {
                var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            try
            {
                var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
                return Ok(blogPost);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Blog yazısı ID {id} bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("author/{authorId:int}")]
        [ProducesResponseType(typeof(List<BlogPostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
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
        [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostDto blogPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userName = User.GetUserName();
                var createdBlogPost = await _blogPostService.AddBlogPostAsync(blogPostDto, userName);
                return CreatedAtAction(nameof(GetBlogPostById), new { id = createdBlogPost.Id }, createdBlogPost);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{blogId:int}")]
        [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> UpdateBlogPost(int blogId, [FromBody] UpdateBlogPostDto blogPostDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string userName = User.GetUserName();

                var updatedBlogPost = await _blogPostService.UpdateBlogPostAsync(blogId, blogPostDto, userName);

                return Ok(updatedBlogPost);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Blog yazısı ID {blogId} bulunamadı");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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
        [Authorize]
        public async Task<IActionResult> DeleteBlogPost(int postId)
        {
            try
            {

                string userName = User.GetUserName();
                var deletedBlogPost = await _blogPostService.DeleteBlogPostAsync(postId, userName);


                return Ok(deletedBlogPost);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Blog yazısı ID {postId} bulunamadı");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
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

        [HttpGet("book/{bookId:int}")]
        [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBlogPostByBookAsync(int bookId)
        {
            try
            {
                var blogPosts = await _blogPostService.GetBlogPostByBook(bookId);
                return Ok(blogPosts);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}