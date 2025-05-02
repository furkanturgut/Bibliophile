using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Interface
{
    public interface IPostLikeRepository
    {
        /// <summary>
        /// Tüm blog yazısı beğenilerini getirir
        /// </summary>
        Task<List<PostLike>> GetAllPostLikesAsync();
        
        /// <summary>
        /// Bir blog yazısının tüm beğenilerini getirir
        /// </summary>
        Task<List<PostLike>> GetLikesByPostIdAsync(int postId);
        
        /// <summary>
        /// Bir kullanıcının beğendği tüm blog yazılarını getirir
        /// </summary>
        Task<List<PostLike>> GetPostLikesByUserIdAsync(string userId);
        
        /// <summary>
        /// Kullanıcının bir blog yazısını beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<PostLike> GetPostLikeByUserAndPostAsync(string userId, int postId);
        
        /// <summary>
        /// Yeni bir beğeni ekler
        /// </summary>
        Task<PostLike> AddPostLikeAsync(PostLike postLike);
        
        /// <summary>
        /// Bir beğeniyi kaldırır
        /// </summary>
        Task<PostLike> RemovePostLikeAsync(PostLike postLike);
        
        /// <summary>
        /// Bir blog yazısının beğeni sayısını getirir
        /// </summary>
        Task<int> GetPostLikesCountAsync(int postId);
        
        /// <summary>
        /// Kullanıcının bir blog yazısını beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> IsPostLikedByUserAsync(string userId, int postId);
    }
}