using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.BlogPostDtos;

namespace Backend.Interface
{
    public interface IBlogPostService
    {
       
        Task<BlogPostDto?> GetBlogPostByIdAsync(int id);
        Task<List<BlogPostDto>> GetAllBlogPostsAsync();
        Task<List<BlogPostDto>> GetBlogPostsByAuthorIdAsync(int authorId);
        Task<BlogPostDto> AddBlogPostAsync(CreateBlogPostDto blogPostDto, string userName);
        Task<BlogPostDto?> UpdateBlogPostAsync(int blogId, UpdateBlogPostDto blogPostDto, string userName);
        Task<BlogPostDto?> DeleteBlogPostAsync(int postId, string userName);
        Task<bool> BlogPostExistsAsync(int id);
        
     
    }
}