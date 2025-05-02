using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Interface
{
    public interface IPostLikeService
    {
        /// <summary>
        /// Bir blog yazısının beğeni sayısını getirir
        /// </summary>
        Task<int> GetPostLikesCountAsync(int postId);
        
        /// <summary>
        /// Kullanıcının bir blog yazısını beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> IsPostLikedByUserAsync(string userId, int postId);
        
        /// <summary>
        /// Bir blog yazısını beğenir veya beğeniyi kaldırır (toggle)
        /// </summary>
        Task<bool> TogglePostLikeAsync(string userId, int postId);
    }
}