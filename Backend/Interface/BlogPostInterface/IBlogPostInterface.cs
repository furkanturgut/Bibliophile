using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dtos.BlogPostDtos;

namespace Backend.Interface
{
    public interface IBlogPostService
    {
        /// <summary>
        /// Belirtilen ID'ye sahip blog yazısını getirir
        /// </summary>
        Task<BlogPostDto?> GetBlogPostByIdAsync(int id);
        
        /// <summary>
        /// Tüm blog yazılarını getirir
        /// </summary>
        Task<List<BlogPostDto>> GetAllBlogPostsAsync();
        
        /// <summary>
        /// Belirtilen kullanıcının blog yazılarını getirir
        /// </summary>
        Task<List<BlogPostDto>> GetBlogPostsByAuthorIdAsync(int authorId);
        
        /// <summary>
        /// Yeni bir blog yazısı ekler
        /// </summary>
        Task<BlogPostDto> AddBlogPostAsync(CreateBlogPostDto blogPostDto, string userId);
        
        /// <summary>
        /// Var olan bir blog yazısını günceller
        /// </summary>
        Task<BlogPostDto?> UpdateBlogPostAsync(int id, UpdateBlogPostDto blogPostDto);
        
        /// <summary>
        /// Bir blog yazısını siler
        /// </summary>
        Task<BlogPostDto?> DeleteBlogPostAsync(int id);
        
        /// <summary>
        /// Blog yazısının var olup olmadığını kontrol eder
        /// </summary>
        Task<bool> BlogPostExistsAsync(int id);
        
     
    }
}