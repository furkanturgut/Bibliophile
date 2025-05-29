using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetBlogPostByIdAsync(int id);
        Task<List<BlogPost>> GetAllBlogPostsAsync();
        Task<List<BlogPost>> GetBlogPostsByAuthorIdAsync(int authorId);
        Task<BlogPost> AddBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogPostAsync(BlogPost blogPost);
        Task<bool> BlogPostExistsAsync(int id);
        Task<List<BlogPost>> GetBlogPostByBook(int bookId);
      
    }
}